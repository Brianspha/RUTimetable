using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TK.CustomMap;
using Xamarin.Forms;

namespace RUTimetable
{
	public partial class PinsListPage : ContentPage
	{
		public event EventHandler<PinSelectedEventArgs> PinSelected;

		private readonly IEnumerable<TKCustomMapPin> Pins;


		public PinsListPage(IEnumerable<TKCustomMapPin> pins)
		{
			InitializeComponent();

			Pins = pins;
			this.BindingContext = this.Pins;

			lvPins.ItemSelected += (o, e) =>
			{
				if (this.lvPins.SelectedItem == null) return;

				this.OnPinSelected((TKCustomMapPin)this.lvPins.SelectedItem);
			};
		}
		protected virtual void OnPinSelected(TKCustomMapPin pin)
		{
			this.PinSelected?.Invoke(this, new PinSelectedEventArgs(pin));
		}
	}

}