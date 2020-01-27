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
					FullName="Aaberg, Jesper",
					Email="someone@example.com",
					ProfileBackground=new BitmapImage(new Uri("ms-appx:/SampleData/SampleUsers/SampleUsers_Files/gradient_002.jpg")),
					Address="4567 Main St., Buffalo, NY 98052",
					PhoneNumber="(111) 555-0100",
					MemberSince="November 5, 2003",
					IsActive=true,
					Company="A. Datum Corporation",
					ProfilePic=new BitmapImage(new Uri("ms-appx:/SampleData/SampleUsers/SampleUsers_Files/profile_006.png")),
					Places = new List<Place>()
					{
						new Place() { Name="Aenean cras", Description="Curabitur suspendisse nam sed phasellus consequat aenean convallis cras pellentesque dis maecenas duis consectetuer aliquam vestibulum integer mauris adipiscing quisque dignissim est pellentesque parturient class vivamus sollicitudin curae condimentum elementum scelerisque facilisis hac vestibulum vestibulum fermentum ullamcorper fringilla nullam nunc habitasse praesent pellentesque accumsan bibendum dictumst leo donec aliquam eleifend consectetuer mus nec etiam amet hendrerit suspendisse fusce adipiscing lorem parturient non vestibulum aliquet ante blandit facilisi aptent arcu condimentum scelerisque vestibulum pellentesque faucibus adipiscing per diam commodo eget parturient auctor himenaeos ullamcorper morbi vestibulum vestibulum", Image=new BitmapImage(new Uri("ms-appx:/SampleData/SampleUsers/SampleUsers_Files/place_00010.jpg")), Location="4567 Main St., Buffalo, NY 98052", Visited="November 5, 2003" },
						new Place() { Name="Curabitur duis", Description ="Imperdiet adipiscing malesuada elit suspendisse habitant sollicitudin enim conubia penatibus condimentum cubilia dapibus erat sed porttitor eros inceptos tincidunt nibh sem interdum lobortis sit nascetur egestas vel nam scelerisque nisi nisl euismod parturient congue ullamcorper vestibulum tristique pellentesque sed nulla vestibulum suspendisse nunc dis proin ultricies consectetuer est venenatis adipiscing feugiat augue", Image =new BitmapImage(new Uri("ms-appx:/SampleData/SampleUsers/SampleUsers_Files/place_00011.jpg")), Location ="1234 Main St., Buffalo, NY 98052", Visited ="December 29, 2006" }
					}
				},
				new User()
				{
					FullName="Adams, Ellen",
					Email="user@adventure-works.com",
					ProfileBackground =new BitmapImage(new Uri("ms-appx:/SampleData/SampleUsers/SampleUsers_Files/gradient_003.jpg")),
					Address ="1234 Main St., Buffalo, NY 98052",
					PhoneNumber ="(222) 555-0101",
					MemberSince ="December 29, 2006",
					IsActive =false,
					Company ="Adventure Works",
					ProfilePic =new BitmapImage(new Uri("ms-appx:/SampleData/SampleUsers/SampleUsers_Files/profile_001.jpg")),
					Places = new List<Place>()
					{
						new Place() { Name="Consectetuer curae", Image=new BitmapImage(new Uri("ms-appx:/SampleData/SampleUsers/SampleUsers_Files/place_00012.jpg")), Visited ="May 27, 2007", Location ="2345 Front St., Seattle, WA 12345", Description ="Parturient tempor vestibulum pellentesque tempus tortor pede consequat vestibulum est sollicitudin quam hac pellentesque leo cubilia consectetuer turpis suspendisse quis dapibus nulla convallis varius dignissim senectus adipiscing condimentum mus proin elementum sociosqu egestas scelerisque parturient pellentesque vestibulum euismod urna sollicitudin augue feugiat nec dolor suscipit pellentesque aenean cras duis vestibulum nunc amet facilisis mauris gravida" },
						new Place() { Name="Vestibulum nunc", Image =new BitmapImage(new Uri("ms-appx:/SampleData/SampleUsers/SampleUsers_Files/place_00013.jpg")), Visited ="May 24, 2006", Location ="789 Front St., Seattle, WA 12345", Description ="Iaculis adipiscing lacinia fames ullamcorper torquent nullam fermentum laoreet natoque posuere fringilla felis ante arcu habitasse ultrices diam consectetuer suspendisse non ipsum aptent parturient hendrerit eget himenaeos imperdiet auctor elit potenti condimentum per congue cursus enim dictum erat lectus libero pellentesque scelerisque vehicula vestibulum eros ligula nibh sollicitudin sed sem volutpat sit vestibulum maecenas pretium nisi litora malesuada praesent penatibus ullamcorper porttitor justo luctus vel accumsan bibendum suspendisse condimentum pellentesque magnis rhoncus adipiscing dictumst consectetuer nisl parturient eleifend tincidunt mattis nam vestibulum tristique" },
						new Place() { Name="Sollicitudin", Image=new BitmapImage(new Uri("ms-appx:/SampleData/SampleUsers/SampleUsers_Files/place_00014.jpg")), Visited="September 19, 2004", Location="4567 Lass St., Seattle, WA 12345", Description="Mauris facilisi sodales nunc faucibus pellentesque lacus scelerisque ultricies habitant venenatis inceptos lorem vulputate odio vestibulum interdum adipiscing magna orci sed curabitur pede massa quam lobortis mollis viverra parturient dis metus phasellus quis est sollicitudin vestibulum consequat aliquam pellentesque consectetuer nascetur hac vestibulum ullamcorper urna leo convallis pharetra placerat suspendisse mus pulvinar adipiscing sagittis senectus dignissim integer cras pellentesque nec elementum parturient non facilisis montes per nostra morbi fermentum ornare sed duis vestibulum platea sociosqu sollicitudin pellentesque neque nunc amet consectetuer vestibulum condimentum pellentesque sollicitudin fringilla scelerisque" }
					}
				},
				new User()
				{
					FullName="Aaron, Lasper",
					Email="someone@example.com",
					ProfileBackground =new BitmapImage(new Uri("ms-appx:/SampleData/SampleUsers/SampleUsers_Files/gradient_004.jpg")),
					Address ="2345 Main St., Buffalo, NY 98052",
					PhoneNumber ="(333) 555-0102",
					MemberSince ="January 19, 2004",
					IsActive =true,
					Company ="Adventure Works",
					ProfilePic=new BitmapImage(new Uri("ms-appx:/SampleData/SampleUsers/SampleUsers_Files/profile_0010.jpg")),
					Places = new List<Place>()
					{
						new Place() { Name="Aliquam", Image =new BitmapImage(new Uri("ms-appx:/SampleData/SampleUsers/SampleUsers_Files/place_00014.jpg")), Visited ="August 9, 2006", Location ="789 1st Ave, San Francisco, CA 99999", Description ="Facilisi rhoncus dictum suspendisse sodales quam lectus leo quis nulla habitasse viverra faucibus mus hendrerit condimentum urna nec pellentesque himenaeos proin vestibulum non habitant per imperdiet cras sollicitudin augue scelerisque aliquam dolor malesuada pellentesque ullamcorper inceptos consectetuer pellentesque integer sollicitudin quisque pellentesque duis fames nunc interdum felis consectetuer libero penatibus porttitor suspendisse amet lobortis ante condimentum ligula scelerisque ullamcorper suspendisse sed vivamus condimentum ipsum scelerisque aliquam adipiscing nascetur justo arcu pellentesque" },
						new Place() { Name="Vestibulum", Image =new BitmapImage(new Uri("ms-appx:/SampleData/SampleUsers/SampleUsers_Files/place_00015.jpg")), Visited ="February 25, 2008", Location ="4567 1st Ave, San Francisco, CA 99999", Description ="Lacus pharetra sem diam lorem tincidunt ullamcorper aliquet placerat litora magna tristique ultricies massa pulvinar sit eget suspendisse sollicitudin metus elit vel venenatis pellentesque nam enim condimentum luctus vulputate parturient curabitur phasellus erat consequat magnis mattis sed blandit scelerisque morbi sagittis commodo mauris conubia ullamcorper vestibulum neque consectetuer mollis montes convallis vestibulum netus pellentesque suspendisse eros condimentum nibh nisi nisl adipiscing cubilia parturient vestibulum vestibulum senectus dapibus sollicitudin pellentesque nulla consectetuer dignissim" },
						new Place() { Name="Class", Image=new BitmapImage(new Uri("ms-appx:/SampleData/SampleUsers/SampleUsers_Files/place_00016.jpg")), Visited="July 20, 2008", Location="1234 Main St., San Francisco, CA 99999", Description="Pellentesque adipiscing elementum facilisis parturient porta nunc purus sollicitudin pellentesque dis nostra est consectetuer ornare risus scelerisque odio egestas pellentesque velit vestibulum euismod hac vestibulum sociosqu adipiscing vitae platea primis leo orci rutrum sollicitudin ullamcorper suscipit class pede feugiat fermentum pellentesque suspendisse quam condimentum gravida sapien fringilla parturient quis urna mus vestibulum vestibulum consectetuer iaculis cras curae torquent pellentesque donec lacinia scelerisque sollicitudin habitasse pellentesque consectetuer pellentesque duis nunc ullamcorper nec adipiscing parturient vestibulum sollicitudin semper hendrerit himenaeos imperdiet sociis suspendisse pellentesque non amet etiam fusce taciti tellus ultrices malesuada tempor penatibus lorem ante morbi arcu consectetuer vehicula pellentesque laoreet" }
					}
				},
				new User()
				{
					FullName ="Albert, Cather",
					Email ="user@adventure-works.com",
					ProfileBackground =new BitmapImage(new Uri("ms-appx:/SampleData/SampleUsers/SampleUsers_Files/gradient_005.jpg")),
					Address ="789 Roam St., NY, NY 98052",
					PhoneNumber ="(444) 555-0103",
					MemberSince ="December 22, 2007",
					IsActive =false,
					Company ="Alpine Ski House",
					ProfilePic =new BitmapImage(new Uri("ms-appx:/SampleData/SampleUsers/SampleUsers_Files/profile_0011.jpg")),
					Places = new List<Place>()
					{
						new Place() { Name="Cras nam", Image=new BitmapImage(new Uri("ms-appx:/SampleData/SampleUsers/SampleUsers_Files/place_00016.jpg")), Visited="December 11, 2007", Location="4567 22nd St NE, Miami, FL 98052", Description="Porta habitasse condimentum quisque scelerisque sollicitudin vivamus purus senectus pellentesque sociosqu suscipit aliquam torquent leo ullamcorper nisl aliquet risus nunc hendrerit velit consectetuer ultrices parturient vehicula vitae mus nec magnis suspendisse volutpat mattis himenaeos maecenas class curae vestibulum pellentesque vestibulum donec etiam non adipiscing condimentum scelerisque per sed fusce blandit odio praesent orci lorem morbi sollicitudin parturient nulla imperdiet" },
						new Place() { Name="Sed maecenas", Image=new BitmapImage(new Uri("ms-appx:/SampleData/SampleUsers/SampleUsers_Files/place_00017.jpg")), Visited="September 26, 2003", Location="1234 22nd St NE, Miami, FL 98052", Description="Mauris malesuada mollis proin augue sem sit pede vestibulum vel vestibulum accumsan montes bibendum dictumst pellentesque nostra dolor quam adipiscing ullamcorper eleifend fames suspendisse penatibus quis felis ornare urna commodo facilisi faucibus consectetuer cras habitant nam pellentesque sed condimentum porttitor scelerisque tincidunt ipsum sollicitudin inceptos ullamcorper suspendisse dis tristique platea condimentum parturient interdum conubia scelerisque ullamcorper cubilia lobortis ultricies est suspendisse venenatis hac primis nascetur duis pharetra justo pellentesque dapibus consectetuer rutrum egestas placerat pellentesque nunc sapien lacus lorem vestibulum magna massa vestibulum condimentum pulvinar" },
						new Place() { Name="Duis", Image=new BitmapImage(new Uri("ms-appx:/SampleData/SampleUsers/SampleUsers_Files/place_00018.jpg")), Visited="April 24, 2001", Location="2345 22nd St NE, Miami, FL 98052", Description="Scelerisque leo sagittis metus adipiscing vulputate semper sociis parturient ullamcorper amet curabitur suspendisse euismod feugiat morbi phasellus mus ante taciti senectus neque nec sociosqu consequat suscipit arcu tellus condimentum scelerisque convallis ullamcorper netus vestibulum vestibulum non per sollicitudin nulla suspendisse torquent sed adipiscing dignissim parturient ultrices gravida condimentum vestibulum vehicula volutpat diam elementum iaculis sem maecenas sit vel praesent porta lacinia nam laoreet scelerisque purus risus natoque sed" },
						new Place() { Name="Vestibulum", Image=new BitmapImage(new Uri("ms-appx:/SampleData/SampleUsers/SampleUsers_Files/place_00019.jpg")), Visited="September 30, 2005", Location="4567 Main St., Buffalo, NY 98052", Description="Vestibulum tempor eget adipiscing ullamcorper velit parturient posuere potenti tempus vestibulum elit dis tortor accumsan turpis vitae bibendum class vestibulum adipiscing enim est hac varius pellentesque pretium rhoncus dictumst consectetuer sodales viverra leo aliquam erat integer quisque suspendisse curae aenean parturient eleifend eros vestibulum donec vivamus etiam mus facilisi nibh faucibus vestibulum nec facilisis mauris habitant nullam non aptent condimentum scelerisque fusce pellentesque inceptos ullamcorper lorem per interdum morbi sed sollicitudin aliquam nulla pellentesque sem" },
						new Place() { Name="Dis nunc", Image=new BitmapImage(new Uri("ms-appx:/SampleData/SampleUsers/SampleUsers_Files/place_0002.jpg")), Visited="April 7, 2001", Location="1234 Main St., Buffalo, NY 98052", Description="Nisi consectetuer auctor adipiscing pellentesque parturient congue sollicitudin cursus dictum vestibulum aliquet blandit nisl lobortis nunc lectus nascetur libero commodo conubia fermentum fringilla suspendisse sit ligula habitasse vestibulum pharetra proin odio cubilia vel hendrerit orci himenaeos pellentesque litora luctus pede dapibus magnis imperdiet quam quis adipiscing malesuada egestas parturient consectetuer placerat condimentum pellentesque euismod pulvinar sagittis mattis feugiat vestibulum penatibus urna senectus" }
					}
				},
				new User()
				{
					FullName="Pfeiffer, Michael",
					Email="user-4@fabrikam.com",
					ProfileBackground=new BitmapImage(new Uri("ms-appx:/SampleData/SampleUsers/SampleUsers_Files/gradient_009.jpg")),
					Address="4567 Main St., Seattle, WA 12345",
					PhoneNumber="(555) 555-0108",
					MemberSince="September 19, 2004",
					IsActive=false,
					Company="Fourth Coffee",
					ProfilePic=new BitmapImage(new Uri("ms-appx:/SampleData/SampleUsers/SampleUsers_Files/profile_0015.jpg")),
					Places = new List<Place>()
					{
						new Place() { Name="Etiam fusce", Image=new BitmapImage(new Uri("ms-appx:/SampleData/SampleUsers/SampleUsers_Files/place_00024.jpg")), Visited="August 4, 2002", Location="2345 22nd St NE, Miami, FL 98052", Description="Erat risus parturient eros nibh velit suspendisse sapien non viverra condimentum per venenatis sed scelerisque torquent aliquam semper vitae vulputate ullamcorper integer curabitur sem pellentesque sollicitudin sit class quisque nisi nisl suspendisse ultrices vehicula nunc vivamus curae pellentesque consectetuer odio orci sociis condimentum pede quam vel aliquam vestibulum pellentesque taciti aliquet quis donec sollicitudin volutpat phasellus consequat vestibulum etiam urna fusce maecenas pellentesque consectetuer lorem nam praesent sed scelerisque blandit morbi commodo conubia cras" },
						new Place() { Name="Dictumst", Image=new BitmapImage(new Uri("ms-appx:/SampleData/SampleUsers/SampleUsers_Files/place_0003.jpg")), Visited="May 28, 2005", Location="4567 Main St., Buffalo, NY 98052", Description="Accumsan bibendum convallis cubilia pellentesque ullamcorper dapibus tellus tempor duis adipiscing dis dignissim dictumst sollicitudin egestas parturient eleifend vestibulum vestibulum pellentesque facilisi euismod consectetuer suspendisse est feugiat tempus condimentum gravida hac pellentesque adipiscing leo tortor mus nec nulla parturient turpis vestibulum sollicitudin proin vestibulum augue dolor iaculis non nunc varius adipiscing parturient elementum facilisis amet per sed vestibulum faucibus scelerisque lacinia sem laoreet pellentesque sit vestibulum fames habitant felis consectetuer natoque inceptos ullamcorper posuere" },
						new Place() { Name="Lorem pellentesque", Image=new BitmapImage(new Uri("ms-appx:/SampleData/SampleUsers/SampleUsers_Files/place_0004.jpg")), Visited="July 7, 2006", Location="1234 Main St., Buffalo, NY 98052", Description="Vel nam aenean fermentum adipiscing fringilla habitasse potenti ipsum interdum parturient mauris nullam sed pellentesque aptent sollicitudin justo hendrerit suspendisse pellentesque himenaeos lobortis lacus vestibulum pretium consectetuer condimentum vestibulum lorem pellentesque magna sollicitudin massa imperdiet ante adipiscing rhoncus dis pellentesque metus consectetuer parturient scelerisque morbi vestibulum sodales neque malesuada pellentesque sollicitudin arcu ullamcorper pellentesque nascetur auctor pharetra est consectetuer netus penatibus suspendisse porttitor hac tincidunt placerat viverra diam congue condimentum pulvinar sagittis nulla tristique ultricies pellentesque scelerisque venenatis leo sollicitudin mus vestibulum porta ullamcorper purus" },
						new Place() { Name="Morbi", Image=new BitmapImage(new Uri("ms-appx:/SampleData/SampleUsers/SampleUsers_Files/place_0005.jpg")), Visited="March 7, 2003", Location="2345 Main St., Buffalo, NY 98052", Description="Adipiscing cursus pellentesque vulputate dictum senectus parturient aliquam vestibulum lectus integer risus vestibulum velit adipiscing libero vitae consectetuer suspendisse ligula litora pellentesque class luctus sociosqu quisque sollicitudin pellentesque nec eget consectetuer suscipit condimentum parturient scelerisque curae vivamus aliquam non donec ullamcorper elit aliquet magnis enim vestibulum erat mattis mauris torquent vestibulum adipiscing blandit commodo pellentesque suspendisse per ultrices eros condimentum curabitur parturient conubia phasellus cubilia scelerisque dapibus sollicitudin egestas vehicula ullamcorper suspendisse condimentum volutpat euismod etiam feugiat pellentesque nibh scelerisque mollis maecenas consequat gravida iaculis nisi nisl montes ullamcorper sed nunc consectetuer" }
					}
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