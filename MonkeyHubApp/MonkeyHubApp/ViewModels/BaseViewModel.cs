using Xamarin.Forms;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MonkeyHubApp.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(storage, value))
                return false;

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        public async Task PushAsync<TViewModel>(params object[] args) where TViewModel : BaseViewModel
        {
            var viewModelType = typeof(TViewModel); //pega o tipo de viewModel vindo como parâmetro. Ex.: MonkeyHubApp.ViewModels.BaseViewModel

            var viewModelTypeName = viewModelType.Name; //pega o nome do tipo da viewModel vinda como parâmetro. Ex.: AboutViewModel
            var viewModelWordLength = "ViewModel".Length; //guarda o tamanho da palavra viewModel
            var viewTypeName = $"MonkeyHubApp.{viewModelTypeName.Substring(0, viewModelTypeName.Length - viewModelWordLength)}Page"; //Retira a palavra viewModel e coloca a palavra page. Ex.: AboutViewModel -> AboutPage
            var viewType = Type.GetType(viewTypeName); //Pega o tipo da view

            var page = Activator.CreateInstance(viewType) as Page; //Cria uma instância da view 
            var viewModel = Activator.CreateInstance(viewModelType, args); // Cria uma instância da viewModel

            if (page != null)
            {
                page.BindingContext = viewModel; //atribui a viewModel ao contexto de binding da nova page
            }

            await Application.Current.MainPage.Navigation.PushAsync(page); // coloca a nova page na pilha de navegação
        }
    }
}