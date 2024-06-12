using BullsAndCows.Infrastructure;
using BullsAndCows.Interfaces;
using BullsAndCows.ViewModels.Base;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BullsAndCows.ViewModels
{
    public class MainViewModel(IEntityService<ScoreViewModel> entityService) : BaseViewModel
    {

        private readonly IEntityService<ScoreViewModel> _entityService = entityService;

        public BaseViewModel? ViewModel { get; set; }

        private async void WriteScore()
        {
            ScoreViewModel svm = new() { Player = "player", Score = Score };
            await _entityService.Add(svm);
        }

        private double _score = 0;

        public double Score
        {
            get => _score;
            set => Set(ref _score, value);            
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

        public ObservableCollection<string> Results { get; set; } = [];

        private string _title = "Bulls and cows";
        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
        }

        private string _enter = string.Empty;
        public string Enter
        {
            get => _enter;
            set
            {
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


        private ICommand? enterDigitCmd;
        public ICommand EnterDigitCmd => enterDigitCmd ??= new LambdaCommand(EnterDigitCmdExecute, CanEnterDigitExecutedCmd);

        private ICommand? setAnswerCmd;
        public ICommand SetAnswerCmd => setAnswerCmd ??= new LambdaCommand(SetAnswerCmdExecute, CanSetAnswerCmdExecuted);

        private ICommand? lookAtRatingCmd;
        public ICommand LookAtRatingCmd => lookAtRatingCmd ??= new LambdaCommand(LookAtRatingCmdExecute, CanLookAtRatingCmdExecuted);

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

        private void LookAtRatingCmdExecute(object parameter)
        {
            ViewModel = new ListScoreViewModel(_entityService);
        }

        private bool CanLookAtRatingCmdExecuted(object parameter)
        {
            if(ViewModel is not ListScoreViewModel)
            {
                return true;
            }

            return false;
        }
    }
}