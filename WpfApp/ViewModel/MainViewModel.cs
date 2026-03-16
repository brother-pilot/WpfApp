using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using WpfApp.DI;
using WpfApp.Models;

namespace WpfApp.ViewModel
{
    internal class MainViewModel
    {
        private readonly ICsvLoader _csvLoader;
        private readonly IGrouper _grouper;
        private readonly IValidator _validator;
        private readonly ISummaryService _summaryService;
        private readonly IFileSaver _fileSaver;

        public ObservableCollection<CsvData> Items { get; } = new();
        public ObservableCollection<Well> Groups { get; } = new();
        public ObservableCollection<Error> Errors { get; } = new();
        public ObservableCollection<SummaryWell> Summary { get; } = new();

        private string _status;
        public string Status { get => _status; set { _status = value; OnPropertyChanged(); } }

        // простые команды
        public ICommand LoadCommand { get; }
        public ICommand GroupCommand { get; }
        public ICommand ValidateCommand { get; }
        public ICommand GenerateSummaryCommand { get; }
        public ICommand ExportCommand { get; }

        public MainViewModel(
            ICsvLoader csvLoader,
            IGrouper grouper,
            IValidator validator,
            ISummaryService summaryService,
            IFileSaver fileSaver)
        {
            _csvLoader = csvLoader;
            _grouper = grouper;
            _validator = validator;
            _summaryService = summaryService;
            _fileSaver = fileSaver;

            LoadCommand = new RelayCommand<string>(async p => await LoadAsync(p));
            GroupCommand = new RelayCommand(async () => await GroupAsync());
            ValidateCommand = new RelayCommand(async () => await ValidateAsync());
            GenerateSummaryCommand = new RelayCommand(async () => await GenerateSummaryAsync());
            ExportCommand = new RelayCommand(async () => await ExportAsync());
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

        private async Task GroupAsync()
        {
            Status = "Grouping...";
            var res = _grouper.GroupeWell(Items);
            foreach (var it in res)
                Groups.Add(it);
            Status = $"Loaded{ Groups.Count} rows.";
        }

        private async Task ValidateAsync()
        {
            Status = "Validating...";
            int valid = 0;
            int total = Groups.Count;
            int numStr = 0;
            foreach (var w in Groups)
            {
                var res = _validator.Validate(w);
                numStr++;
                if (!res.IsValid)
                    Errors.Add(new Error(numStr, w.WellId, res.ToString()));
                if (res.IsValid) 
                    valid++;
            }
            Status = $"Validation complete: {valid}/{total} valid.";
            // можно пометить неверные строки как ошибочные в UI
        }

        private async Task GenerateSummaryAsync()
        {
            Status = "Generating summary...";
            Summary.Clear();
            var summary = _summaryService.BuildSummary(Groups);
            foreach (var s in summary)
                Summary.Add(s);
            Status = $"Summary: {Summary.Count} rows.";
        }

        private async Task ExportAsync()
        {
            Status = "Exporting...";
            var res=_fileSaver.SaveFile(Summary);
            if (res==true)
                Status = "Export completed.";
            else
                Status = "Export failed.";
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    }
}
