using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Documents;
using System.Windows.Input;

namespace WpfTesty
{
    public class CitiesViewModel: INotifyPropertyChanged
    {
        private string computedResult;
        private Vertex selection1;
        private Vertex selection2;
        private Edge selectedEdge;
        private float inputDistance;
        private CitiesModel model;

        public IReadOnlyCollection<Vertex> CitiesList { get; private set; }

        public CitiesViewModel() {
            model = new();
            OnPropertyChanged(nameof(DataView));
        }


        public string ComputedResult {
            get => computedResult;
            set {
                computedResult = value;
                OnPropertyChanged(nameof(ComputedResult));
            }
        }

        public Vertex Selection1 {
            get => selection1;
            set {
                selection1 = value;
                UpdateSelectedEdge();
                OnPropertyChanged(nameof(Selection1));
            }
        }

        public Vertex Selection2 {
            get => selection2;
            set {
                selection2 = value;
                UpdateSelectedEdge();
                OnPropertyChanged(nameof(Selection2));
            }
        }

        public Edge SelectedEdge {
            get => selectedEdge;
            set {
                selectedEdge = value;
                OnPropertyChanged(nameof(SelectedEdge));
            }
        }

        private void UpdateSelectedEdge() {
            SelectedEdge = model.GetEdge(Selection1, Selection2);

            if (SelectedEdge != null)
                InputDistance = SelectedEdge.Distance;
        }

        public float InputDistance {
            get => inputDistance;
            set {
                if (value > 0)
                    inputDistance = value;

                if (SelectedEdge is not null)
                    SelectedEdge.Distance = value;

                OnPropertyChanged(nameof(InputDistance));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new(propertyName));
        }


        #region Commands

        private RelayCommand addCommand;
        private RelayCommand removeCommand;
        private RelayCommand calculateCommand;


        public ICommand AddCommand =>
            addCommand ??= new(_ => AddNewCity(), _ => true);

        public ICommand RemoveCommand =>
            removeCommand ??= new(_ => RemoveSelectedCity(), _ => CanRemove());

        private bool CanRemove() {
            return Selection1 is not null && CitiesList.Contains(Selection1);
        }

        public ICommand CalculateCommand =>
            calculateCommand ??= new(_ => Calculate(), _ => CanCalculate());

        private bool CanCalculate() {
            return CitiesList?.Count >= 2;
        }

        private void Calculate() {
            var shortestRoute = model.GetShortestRoute();
            ComputedResult = string.Join(" > ", shortestRoute);
        }

        private void RemoveSelectedCity() {
            model.Remove(Selection1);
            Selection1 = null;
            CitiesList = new List<Vertex>(model.Vertices);
            OnPropertyChanged(nameof(CitiesList));
        }

        private void AddNewCity() {
            model.Add();
            CitiesList = new List<Vertex>(model.Vertices);
            OnPropertyChanged(nameof(CitiesList));
        }

        #endregion
    }
}