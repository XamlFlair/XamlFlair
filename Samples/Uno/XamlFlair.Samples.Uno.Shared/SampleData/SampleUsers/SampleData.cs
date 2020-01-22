using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace XamlFlair.Samples.Uno.SampleData
{
	public static class SampleData
	{
		public static List<User> Users = new List<User>();

		internal static void InitializeSampleData()
		{
			Users.AddRange(new User[]
			{
				new User()
				{
					Address="4567 Main St., Buffalo, NY 98052",
					Company="A. Datum Corporation",
					Email="someone@example.com",
					FullName="Aaberg, Jesper",
					IsActive=true,
					MemberSince="November 5, 2003",
					PhoneNumber="(111) 555-0100",
					ProfileBackground=new BitmapImage(new Uri("ms-appx:/SampleData/SampleUsers/SampleUsers_Files/gradient_002.jpg")),
					ProfilePic=new BitmapImage(new Uri("ms-appx:/SampleData/SampleUsers/SampleUsers_Files/profile_006.png")),
					Places = new List<Place>()
					{
						new Place()
						{
							Name="Aenean cras",
							Description="Curabitur suspendisse nam sed phasellus consequat aenean convallis cras pellentesque dis maecenas duis consectetuer aliquam vestibulum integer mauris adipiscing quisque dignissim est pellentesque parturient class vivamus sollicitudin curae condimentum elementum scelerisque facilisis hac vestibulum vestibulum fermentum ullamcorper fringilla nullam nunc habitasse praesent pellentesque accumsan bibendum dictumst leo donec aliquam eleifend consectetuer mus nec etiam amet hendrerit suspendisse fusce adipiscing lorem parturient non vestibulum aliquet ante blandit facilisi aptent arcu condimentum scelerisque vestibulum pellentesque faucibus adipiscing per diam commodo eget parturient auctor himenaeos ullamcorper morbi vestibulum vestibulum",
							Image=new BitmapImage(new Uri("ms-appx:/SampleData/SampleUsers/SampleUsers_Files/place_00010.jpg")),
							Location="4567 Main St., Buffalo, NY 98052",
							Visited="November 5, 2003"
						}
					}
				},
				new User()
				{
				}
			});
		}
	}

	public class User : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		private List<Place> _Places;

		public List<Place> Places
		{
			get
			{
				return this._Places;
			}
			set
			{
				if (this.Places != value)
				{
					this._Places = value;
					this.OnPropertyChanged("Places");
				}
			}
		}

		private string _FullName = string.Empty;

		public string FullName
		{
			get
			{
				return this._FullName;
			}

			set
			{
				if (this._FullName != value)
				{
					this._FullName = value;
					this.OnPropertyChanged("FullName");
				}
			}
		}

		private string _Email = string.Empty;

		public string Email
		{
			get
			{
				return this._Email;
			}

			set
			{
				if (this._Email != value)
				{
					this._Email = value;
					this.OnPropertyChanged("Email");
				}
			}
		}

		private ImageSource _ProfileBackground = null;

		public ImageSource ProfileBackground
		{
			get
			{
				return this._ProfileBackground;
			}

			set
			{
				if (this._ProfileBackground != value)
				{
					this._ProfileBackground = value;
					this.OnPropertyChanged("ProfileBackground");
				}
			}
		}

		private string _Address = string.Empty;

		public string Address
		{
			get
			{
				return this._Address;
			}

			set
			{
				if (this._Address != value)
				{
					this._Address = value;
					this.OnPropertyChanged("Address");
				}
			}
		}

		private string _PhoneNumber = string.Empty;

		public string PhoneNumber
		{
			get
			{
				return this._PhoneNumber;
			}

			set
			{
				if (this._PhoneNumber != value)
				{
					this._PhoneNumber = value;
					this.OnPropertyChanged("PhoneNumber");
				}
			}
		}

		private string _MemberSince = string.Empty;

		public string MemberSince
		{
			get
			{
				return this._MemberSince;
			}

			set
			{
				if (this._MemberSince != value)
				{
					this._MemberSince = value;
					this.OnPropertyChanged("MemberSince");
				}
			}
		}

		private bool _IsActive = false;

		public bool IsActive
		{
			get
			{
				return this._IsActive;
			}

			set
			{
				if (this._IsActive != value)
				{
					this._IsActive = value;
					this.OnPropertyChanged("IsActive");
				}
			}
		}

		private string _Company = string.Empty;

		public string Company
		{
			get
			{
				return this._Company;
			}

			set
			{
				if (this._Company != value)
				{
					this._Company = value;
					this.OnPropertyChanged("Company");
				}
			}
		}

		private ImageSource _ProfilePic = null;

		public ImageSource ProfilePic
		{
			get
			{
				return this._ProfilePic;
			}

			set
			{
				if (this._ProfilePic != value)
				{
					this._ProfilePic = value;
					this.OnPropertyChanged("ProfilePic");
				}
			}
		}
	}

	public class Place : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		private string _Name = string.Empty;

		public string Name
		{
			get
			{
				return this._Name;
			}

			set
			{
				if (this._Name != value)
				{
					this._Name = value;
					this.OnPropertyChanged("Name");
				}
			}
		}

		private ImageSource _Image = null;

		public ImageSource Image
		{
			get
			{
				return this._Image;
			}

			set
			{
				if (this._Image != value)
				{
					this._Image = value;
					this.OnPropertyChanged("Image");
				}
			}
		}

		private string _Visited = string.Empty;

		public string Visited
		{
			get
			{
				return this._Visited;
			}

			set
			{
				if (this._Visited != value)
				{
					this._Visited = value;
					this.OnPropertyChanged("Visited");
				}
			}
		}

		private string _Location = string.Empty;

		public string Location
		{
			get
			{
				return this._Location;
			}

			set
			{
				if (this._Location != value)
				{
					this._Location = value;
					this.OnPropertyChanged("Location");
				}
			}
		}

		private string _Description = string.Empty;

		public string Description
		{
			get
			{
				return this._Description;
			}

			set
			{
				if (this._Description != value)
				{
					this._Description = value;
					this.OnPropertyChanged("Description");
				}
			}
		}
	}
}