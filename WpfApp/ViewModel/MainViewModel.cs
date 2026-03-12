using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfApp.DI;
using WpfApp.Models;

namespace WpfApp.ViewModel
{
    internal class MainViewModel
    {
        private readonly ICsvLoader _csvLoader;
        private readonly IValidator<Interval> _validator;
        private readonly ISummaryService _summaryService;

        public ObservableCollection<CsvData> Items { get; } = new();
        public ObservableCollection<ResultWall> Summary { get; } = new();

        private string _status;
        public string Status { get => _status; set { _status = value; OnPropertyChanged(); } }

        // простые команды
        public ICommand LoadCommand { get; }
        public ICommand ValidateCommand { get; }
        public ICommand GenerateSummaryCommand { get; }

        public MainViewModel(
            ICsvLoader csvLoader,
            IValidator<Interval> validator,
            ISummaryService summaryService)
        {
            _csvLoader = csvLoader;
            _validator = validator;
            _summaryService = summaryService;

            LoadCommand = new RelayCommand<string>(async p => await LoadAsync(p));
            ValidateCommand = new RelayCommand(async () => await ValidateAsync());
            GenerateSummaryCommand = new RelayCommand(async () => await GenerateSummaryAsync());
        }

        private async Task LoadAsync(string csvPath)
        {
            Status = "Loading...";
            Items.Clear();
            var list = await _csvLoader.LoadAsync(csvPath);
            foreach (var it in list)
                Items.Add(it);
            Status = $"Loaded {Items.Count} rows.";
        }

        private async Task ValidateAsync()
        {
            Status = "Validating...";
            int valid = 0;
            int total = Items.Count;
            foreach (var it in Items)
            {
                // Здесь можно хранить отдельное свойство ValidationResult, если нужно UI-обновление для каждой строки
                //var res = _validator.Validate(it);
                //if (res.IsValid) valid++;
            }
            Status = $"Validation complete: {valid}/{total} valid.";
            // можно пометить неверные строки как ошибочные в UI
        }

        private async Task GenerateSummaryAsync()
        {
            Status = "Generating summary...";
            Summary.Clear();
            /*var summary = _summaryService.BuildSummary(Items);
            foreach (var s in summary)
                Summary.Add(s);
            Status = $"Summary: {Summary.Count} rows.";*/
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    }
}
