using BullsAndCows.Infrastructure;
using BullsAndCows.Interfaces;
using BullsAndCows.ViewModels.Base;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BullsAndCows.ViewModels
{
    public partial class GameViewModel(IEntityService<ScoreViewModel> entityService) : BaseViewModel
    {
        private readonly IEntityService<ScoreViewModel> _entityService = entityService;

        private double _score = 0;
        public double Score
        {
            get => _score;
            set => Set(ref _score, value);
        }

        private string _playerName = "player";
        public string PlayerName
        {
            get => _playerName;
            set => Set(ref _playerName, value);
        }

        private string _answer = string.Empty;
        public string Answer
        {
            get => _answer;
            set
            {
                Set(ref _answer, value);
                Results.Clear();
            }
        }

        private bool _gameOver = false;
        public bool GameOver
        {
            get => _gameOver;
            set
            {
                Set(ref _gameOver, value);
                if(value == true)
                {
                    Score = 100 / Results.Count + 1;
                    WriteScore();
                    MessageBox.Show($"Число угадано за {Results.Count + 1} попытки");
                    Answer = string.Empty;
                }
                if(value == false)
                {
                    Score = 0;
                }
            }
        }

        private string _enter = string.Empty;
        public string Enter
        {
            get => _enter;
            set
            {
                if(IsNoDigitRegex().IsMatch(value) || Equals(Answer, string.Empty))
                {
                    return;
                }
                if(_enter.Length < 4)
                {
                    Set(ref _enter, value);
                }
                if(_enter.Length == 4)
                {
                    GetResult(Enter);
                    Set(ref _enter, string.Empty);
                }
            }
        }


        public ObservableCollection<string> Results { get; set; } = [];

        private void GetResult(string s)
        {
            int bulls = 0;
            int cows = 0;
            if(Equals(s, Answer))
            {
                bulls = 4;
                cows = 4;
                GameOver = true;
            }
            else
            {
                for(int i = 0; i < s.Length; i++)
                {
                    if(Answer.Contains(s[i]))
                    {
                        cows++;
                    }
                    if(Answer[i] == s[i])
                    {
                        bulls++;
                    }
                }
            }

            Results.Add($"Bulls: {bulls};\tCows: {cows}\t<-- {s}");
        }

        private async void WriteScore()
        {
            ScoreViewModel svm = new() { Player = PlayerName, Score = Score };
            await _entityService.Add(svm);
        }

        #region Cmds


        #region set enter

        private ICommand? enterDigitCmd;
        public ICommand EnterDigitCmd => enterDigitCmd ??= new LambdaCommand(EnterDigitCmdExecute, CanEnterDigitExecutedCmd);

        private void EnterDigitCmdExecute(object parameter)
        {
            var btn = parameter as Button;

            if(!Enter.Contains(btn.Content.ToString()))
            {
                Enter += btn.Content;
            }
        }

        private bool CanEnterDigitExecutedCmd(object parameter)
        {
            if(Equals(Answer, string.Empty))
            {
                return false;
            }
            return true;
        }

        #endregion


        #region set rating

        private ICommand? setAnswerCmd;
        public ICommand SetAnswerCmd => setAnswerCmd ??= new LambdaCommand(SetAnswerCmdExecute, CanSetAnswerCmdExecuted);

        private void SetAnswerCmdExecute(object parameter)
        {
            Random random = new();

            int[] digits = [0, 1, 2, 3, 4, 5, 6, 7, 8, 9];

            while(Answer.Length < 4)
            {
                var position = random.Next(0, digits.Length);
                if(!Answer.Contains(digits[position].ToString()))
                {
                    Answer += digits[position];
                }
            }


            GameOver = false;
        }

        private bool CanSetAnswerCmdExecuted(object parameter)
        {
            if(!Equals(Answer, string.Empty))
            {
                return false;
            }
            return true;
        }

        #endregion

        #endregion


        [GeneratedRegex(@"\D")]
        private static partial Regex IsNoDigitRegex();
    }
}