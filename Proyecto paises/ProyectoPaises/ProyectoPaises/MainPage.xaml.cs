using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ProyectoPaises
{
	public partial class MainPage : ContentPage
	{
        public ObservableCollection<object> Items { get; set; } = new ObservableCollection<object>();

        public MainPage()
		{
			InitializeComponent();
		}
	}
}
