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
					ProfilePic=new BitmapImage(new Uri("ms-appx:/SampleData/SampleUsers/SampleUsers_Files/profile_006.jpg")),
					Places = new List<Place>()
					{
						new Place() { Name="Aenean cras", Description="Curabitur suspendisse nam sed phasellus consequat aenean convallis cras pellentesque dis maecenas duis consectetuer aliquam vestibulum integer mauris adipiscing quisque dignissim est pellentesque parturient class vivamus sollicitudin curae condimentum elementum scelerisque facilisis hac vestibulum vestibulum fermentum ullamcorper fringilla nullam nunc habitasse praesent pellentesque accumsan bibendum dictumst leo donec aliquam eleifend consectetuer mus nec etiam amet hendrerit suspendisse fusce adipiscing lorem parturient non vestibulum aliquet ante blandit facilisi aptent arcu condimentum scelerisque vestibulum pellentesque faucibus adipiscing per diam commodo eget parturient auctor himenaeos ullamcorper morbi vestibulum vestibulum", Image=new BitmapImage(new Uri("ms-appx:/SampleData/SampleUsers/SampleUsers_Files/place_00010.jpg")), Location="4567 Main St., Buffalo, NY 98052", Visited="November 5, 2003" },
						new Place() { Name="Curabitur duis", Description ="Imperdiet adipiscing malesuada elit suspendisse habitant sollicitudin enim conubia penatibus condimentum cubilia dapibus erat sed porttitor eros inceptos tincidunt nibh sem interdum lobortis sit nascetur egestas vel nam scelerisque nisi nisl euismod parturient congue ullamcorper vestibulum tristique pellentesque sed nulla vestibulum suspendisse nunc dis proin ultricies consectetuer est venenatis adipiscing feugiat augue", Image =new BitmapImage(new Uri("ms-appx:/SampleData/SampleUsers/SampleUsers_Files/place_00011.jpg")), Location ="1234 Main St., Buffalo, NY 98052", Visited ="December 29, 2006" },
						new Place() { Name="Aenean suspendisse", Image=new BitmapImage(new Uri("ms-appx:/SampleData/SampleUsers/SampleUsers_Files/place_00012.jpg")), Visited="January 19, 2004", Location="2345 Main St., Buffalo, NY 98052", Description="Vulputate pharetra curabitur hac dolor placerat phasellus cursus pulvinar pellentesque dictum fames gravida lectus parturient odio libero felis sollicitudin ipsum sagittis ligula iaculis justo lacinia laoreet condimentum scelerisque natoque leo litora orci consequat ullamcorper suspendisse pede convallis vestibulum senectus quam lacus dignissim vestibulum condimentum posuere scelerisque elementum ullamcorper suspendisse condimentum facilisis luctus adipiscing lorem parturient magna quis potenti fermentum" },
						new Place() { Name="Maecenas", Image=new BitmapImage(new Uri("ms-appx:/SampleData/SampleUsers/SampleUsers_Files/place_00013.jpg")), Visited="December 22, 2007", Location="789 Main St., Buffalo, NY 98052", Description="Vestibulum vestibulum scelerisque pellentesque pretium fringilla massa sociosqu habitasse suscipit hendrerit rhoncus himenaeos adipiscing ullamcorper consectetuer suspendisse torquent ultrices parturient vehicula condimentum pellentesque sollicitudin volutpat magnis pellentesque mattis sodales mus imperdiet nec metus non scelerisque mauris viverra vestibulum mollis morbi neque netus consectetuer montes nulla nostra per malesuada ullamcorper porta pellentesque penatibus maecenas porttitor urna sollicitudin vestibulum suspendisse ornare sed tincidunt condimentum cras praesent pellentesque accumsan purus bibendum consectetuer dictumst eleifend duis risus platea aliquam velit scelerisque facilisi" },
						new Place() { Name="Class curabitur", Image=new BitmapImage(new Uri("ms-appx:/SampleData/SampleUsers/SampleUsers_Files/place_00014.jpg")), Visited="August 23, 2003", Location="1234 Front St., Seattle, WA 12345", Description="Adipiscing nunc vitae class faucibus curae amet pellentesque integer tristique sollicitudin ullamcorper habitant ultricies primis donec inceptos quisque ante vivamus aliquam venenatis arcu interdum parturient rutrum vulputate vestibulum sapien diam eget suspendisse elit etiam sem enim condimentum aliquet fusce pellentesque semper consectetuer erat curabitur phasellus sit pellentesque sollicitudin scelerisque pellentesque ullamcorper vel eros sociis nibh nam sed vestibulum blandit lorem lobortis taciti nascetur morbi commodo nisi adipiscing tellus pharetra nisl conubia dis nunc odio orci placerat pulvinar sagittis consectetuer" }
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
				},
				new User()
				{
					FullName="Philips, Carol",
					Email="user-5@fabrikam.com",
					ProfileBackground=new BitmapImage(new Uri("ms-appx:/SampleData/SampleUsers/SampleUsers_Files/gradient_002.jpg")),
					Address="1234 1st Ave, San Francisco, CA 99999",
					PhoneNumber="(111) 555-0109",
					MemberSince="August 10, 2003",
					IsActive=true,
					Company="Graphic Design Institute",
					ProfilePic=new BitmapImage(new Uri("ms-appx:/SampleData/SampleUsers/SampleUsers_Files/profile_0016.jpg")),
					Places = new List<Place>()
					{
						new Place() { Name="Vestibulum aenean", Image=new BitmapImage(new Uri("ms-appx:/SampleData/SampleUsers/SampleUsers_Files/place_0004.jpg")), Visited="April 9, 2008", Location="1234 Front St., Seattle, WA 12345", Description="Dis habitant sollicitudin inceptos est vestibulum tellus hac fermentum vestibulum interdum lobortis proin cras nascetur laoreet adipiscing tempor augue parturient fringilla duis scelerisque nunc pharetra vestibulum natoque placerat tempus dolor habitasse amet pulvinar hendrerit leo ullamcorper tortor turpis sagittis pellentesque himenaeos varius suspendisse condimentum posuere fames felis scelerisque mus aenean nec ante consectetuer potenti vestibulum pellentesque adipiscing" },
						new Place() { Name="Ullamcorper adipiscing", Image=new BitmapImage(new Uri("ms-appx:/SampleData/SampleUsers/SampleUsers_Files/place_0005.jpg")), Visited="October 2, 2005", Location="2345 Front St., Seattle, WA 12345", Description="Ullamcorper non ipsum justo suspendisse parturient pretium per rhoncus sed senectus arcu vestibulum sem sodales condimentum mauris sit lacus lorem vel imperdiet magna sociosqu suscipit viverra sollicitudin diam aliquam eget nam pellentesque sed elit torquent massa nullam malesuada scelerisque penatibus integer vestibulum dis metus porttitor adipiscing aptent morbi tincidunt auctor tristique congue quisque ultricies venenatis enim ullamcorper vulputate" },
						new Place() { Name="Dignissim", Image=new BitmapImage(new Uri("ms-appx:/SampleData/SampleUsers/SampleUsers_Files/place_0006.jpg")), Visited="June 4, 2007", Location="789 Front St., Seattle, WA 12345", Description="Curabitur cursus parturient dictum ultrices neque consectetuer phasellus est consequat vehicula erat volutpat convallis netus dignissim suspendisse nulla elementum hac pellentesque condimentum eros vestibulum maecenas leo praesent vestibulum vivamus aliquam aliquet lectus blandit facilisis sollicitudin commodo pellentesque nibh nisi adipiscing porta mus fermentum parturient vestibulum conubia scelerisque consectetuer nisl ullamcorper vestibulum libero cubilia suspendisse adipiscing condimentum purus pellentesque parturient dapibus fringilla risus nunc habitasse odio orci hendrerit scelerisque himenaeos vestibulum imperdiet vestibulum nec sollicitudin adipiscing pellentesque pede egestas non per velit accumsan malesuada penatibus parturient sed ullamcorper porttitor consectetuer quam quis pellentesque ligula vestibulum sem sollicitudin suspendisse sit bibendum" },
						new Place() { Name="Mauris", Image=new BitmapImage(new Uri("ms-appx:/SampleData/SampleUsers/SampleUsers_Files/place_0007.jpg")), Visited="June 14, 2001", Location="4567 Main St., Seattle, WA 12345", Description="Dictumst vestibulum vel litora luctus urna pellentesque magnis eleifend nam condimentum adipiscing scelerisque consectetuer tincidunt vitae sed tristique facilisi pellentesque cras class dis sollicitudin faucibus habitant parturient euismod mattis ullamcorper suspendisse est hac condimentum feugiat curae donec etiam fusce gravida vestibulum vestibulum lorem inceptos leo iaculis mauris mus adipiscing lacinia pellentesque morbi nulla duis mollis proin parturient nec scelerisque vestibulum laoreet interdum consectetuer vestibulum augue adipiscing ultricies pellentesque natoque montes non" },
						new Place() { Name="Parturient", Image=new BitmapImage(new Uri("ms-appx:/SampleData/SampleUsers/SampleUsers_Files/place_0008.jpg")), Visited="February 23, 2006", Location="1234 1st Ave, San Francisco, CA 99999", Description="Ullamcorper nostra nunc amet posuere sollicitudin potenti suspendisse per dolor venenatis vulputate pellentesque condimentum lobortis scelerisque consectetuer nascetur fames pharetra ante ornare placerat pellentesque arcu parturient pulvinar platea felis vestibulum primis curabitur rutrum sapien sed ullamcorper diam sollicitudin pellentesque ipsum justo eget vestibulum pretium suspendisse condimentum sem consectetuer phasellus pellentesque consequat rhoncus lacus adipiscing sollicitudin sagittis scelerisque elit pellentesque parturient sodales enim convallis semper ullamcorper sit sociis senectus vestibulum dignissim vestibulum consectetuer elementum pellentesque taciti sollicitudin sociosqu vel erat tellus facilisis suscipit adipiscing tempor nam pellentesque viverra fermentum" }
					}
				},
				new User()
				{
					FullName="Poe, Toni",
					Email="user-6@fabrikam.com",
					ProfileBackground=new BitmapImage(new Uri("ms-appx:/SampleData/SampleUsers/SampleUsers_Files/gradient_003.jpg")),
					Address="2345 1st Ave, San Francisco, CA 99999",
					PhoneNumber="(222) 555-0110",
					MemberSince="September 20, 2007",
					IsActive=false,
					Company="Humongous Insurance",
					ProfilePic=new BitmapImage(new Uri("ms-appx:/SampleData/SampleUsers/SampleUsers_Files/profile_0017.jpg")),
					Places = new List<Place>()
					{
						new Place() { Name="Elementum", Image=new BitmapImage(new Uri("ms-appx:/SampleData/SampleUsers/SampleUsers_Files/place_0006.jpg")), Visited="November 13, 2007", Location="2345 1st Ave, San Francisco, CA 99999", Description="Aliquam torquent sed consectetuer dis lorem suspendisse eros ultrices integer fringilla parturient vehicula volutpat magna pellentesque massa vestibulum condimentum metus quisque sollicitudin est vestibulum pellentesque habitasse hac scelerisque tempus morbi neque consectetuer maecenas adipiscing leo nibh hendrerit parturient pellentesque tortor nisi nisl turpis mus vivamus varius ullamcorper nunc aenean vestibulum odio mauris nec orci pede praesent accumsan netus non aliquam quam sollicitudin vestibulum nullam nulla himenaeos quis suspendisse aliquet per aptent blandit condimentum commodo sed porta bibendum" },
						new Place() { Name="Eleifend hac", Image=new BitmapImage(new Uri("ms-appx:/SampleData/SampleUsers/SampleUsers_Files/place_0007.jpg")), Visited="April 16, 2002", Location="789 1st Ave, San Francisco, CA 99999", Description="Pellentesque auctor dictumst scelerisque ullamcorper eleifend facilisi urna suspendisse condimentum conubia faucibus habitant imperdiet congue adipiscing cubilia purus risus dapibus sem scelerisque ullamcorper velit consectetuer parturient cursus egestas malesuada euismod penatibus sit pellentesque sollicitudin porttitor pellentesque feugiat inceptos vitae gravida cras class dictum vestibulum vestibulum curae suspendisse lectus consectetuer adipiscing libero duis condimentum scelerisque iaculis vel lacinia ligula ullamcorper nunc donec amet laoreet litora etiam luctus tincidunt tristique nam magnis fusce pellentesque sollicitudin suspendisse lorem condimentum interdum sed natoque ultricies scelerisque pellentesque posuere consectetuer ante potenti arcu diam pretium lobortis rhoncus eget parturient dis elit" },
						new Place() { Name="Diam", Image=new BitmapImage(new Uri("ms-appx:/SampleData/SampleUsers/SampleUsers_Files/place_0008.jpg")), Visited="June 2, 2005", Location="4567 1st Ave, San Francisco, CA 99999", Description="Pellentesque vestibulum sollicitudin nascetur sodales venenatis morbi mattis vulputate viverra aliquam nulla est pharetra placerat pulvinar integer pellentesque enim mauris proin vestibulum quisque augue adipiscing erat sagittis eros senectus consectetuer nibh nisi ullamcorper curabitur dolor nisl sociosqu suscipit hac leo phasellus consequat suspendisse vivamus convallis fames mus torquent pellentesque sollicitudin nec felis ultrices parturient condimentum ipsum mollis dignissim vestibulum justo nunc odio" },
						new Place() { Name="Facilisis", Image=new BitmapImage(new Uri("ms-appx:/SampleData/SampleUsers/SampleUsers_Files/place_0009.jpg")), Visited="June 20, 2001", Location="1234 Main St., San Francisco, CA 99999", Description="Scelerisque vestibulum montes lacus nostra aliquam lorem aliquet non pellentesque ullamcorper orci adipiscing elementum suspendisse consectetuer vehicula volutpat per pellentesque pede maecenas condimentum sollicitudin praesent sed parturient pellentesque vestibulum scelerisque ullamcorper vestibulum suspendisse blandit adipiscing quam quis commodo condimentum facilisis ornare conubia consectetuer urna platea cubilia sem magna primis rutrum cras duis massa metus pellentesque sapien scelerisque semper fermentum morbi parturient neque fringilla nunc amet ante netus nulla sollicitudin vestibulum vestibulum arcu adipiscing dapibus accumsan parturient bibendum" },
						new Place() { Name="Leo facilisi", Image=new BitmapImage(new Uri("ms-appx:/SampleData/SampleUsers/SampleUsers_Files/place_00020.jpg")), Visited="November 5, 2003", Location="2345 Front St., San Francisco, CA 99999", Description="Vestibulum habitasse sit vel dictumst hendrerit egestas vestibulum himenaeos euismod imperdiet eleifend feugiat porta ullamcorper adipiscing malesuada suspendisse nam parturient condimentum sed diam purus scelerisque risus facilisi faucibus pellentesque gravida ullamcorper penatibus velit suspendisse eget vestibulum porttitor vitae class tincidunt curae iaculis lacinia tristique habitant vestibulum laoreet inceptos consectetuer natoque dis interdum posuere pellentesque ultricies adipiscing potenti condimentum donec etiam est scelerisque parturient fusce venenatis sollicitudin sociis vulputate elit hac enim pretium curabitur rhoncus sodales viverra lobortis aliquam vestibulum leo ullamcorper mus phasellus nascetur taciti lorem morbi nulla" }
					}
				},
				new User()
				{
					FullName="Hicks, Cassie",
					Email="user-7@fabrikam.com",
					ProfileBackground=new BitmapImage(new Uri("ms-appx:/SampleData/SampleUsers/SampleUsers_Files/gradient_004.jpg")),
					Address="789 1st Ave, San Francisco, CA 99999",
					PhoneNumber="(111) 555-0120",
					MemberSince="August 9, 2006",
					IsActive=true,
					Company="Lucerne Publishing",
					ProfilePic=new BitmapImage(new Uri("ms-appx:/SampleData/SampleUsers/SampleUsers_Files/profile_0018.jpg")),
					Places = new List<Place>()
					{
						new Place() { Name="Vestibulum", Image=new BitmapImage(new Uri("ms-appx:/SampleData/SampleUsers/SampleUsers_Files/place_0008.jpg")), Visited="December 29, 2006", Location="789 22nd St NE, Miami, FL 98052", Description="Erat tellus proin suspendisse nec pellentesque consequat integer convallis augue non pharetra quisque vestibulum tempor consectetuer dolor dignissim pellentesque per placerat eros nibh sollicitudin sed nisi tempus pellentesque nisl nunc condimentum vivamus adipiscing sem pulvinar parturient vestibulum tortor turpis elementum scelerisque consectetuer ullamcorper varius aliquam odio facilisis sit fermentum fringilla fames vestibulum aenean orci" },
						new Place() { Name="Vivamus", Image=new BitmapImage(new Uri("ms-appx:/SampleData/SampleUsers/SampleUsers_Files/place_0009.jpg")), Visited="January 19, 2004", Location="4567 22nd St NE, Miami, FL 98052", Description="Adipiscing pellentesque felis ipsum parturient suspendisse habitasse hendrerit mauris aliquet blandit sagittis vestibulum nullam justo himenaeos pede sollicitudin lacus lorem quam senectus aptent vestibulum sociosqu pellentesque commodo auctor congue conubia condimentum quis vel suscipit cursus torquent ultrices cubilia vehicula volutpat nam urna imperdiet dictum consectetuer scelerisque lectus libero adipiscing sed maecenas ligula dapibus dis est cras praesent egestas parturient vestibulum euismod magna litora ullamcorper accumsan massa bibendum duis pellentesque vestibulum nunc adipiscing luctus parturient metus hac suspendisse leo condimentum malesuada dictumst amet eleifend morbi sollicitudin facilisi penatibus pellentesque faucibus ante consectetuer mus vestibulum scelerisque nec vestibulum adipiscing ullamcorper" },
						new Place() { Name="Proin aliquam", Image=new BitmapImage(new Uri("ms-appx:/SampleData/SampleUsers/SampleUsers_Files/place_00020.jpg")), Visited="December 22, 2007", Location="1234 22nd St NE, Miami, FL 98052", Description="Parturient porttitor magnis arcu mattis non suspendisse condimentum pellentesque tincidunt scelerisque sollicitudin mauris feugiat habitant per diam mollis vestibulum sed ullamcorper sem inceptos interdum tristique vestibulum adipiscing sit vel lobortis ultricies gravida neque nam montes eget nostra iaculis parturient lacinia laoreet elit venenatis sed enim vulputate suspendisse natoque posuere curabitur phasellus vestibulum dis consequat pellentesque vestibulum est condimentum nascetur pharetra ornare adipiscing scelerisque platea hac primis parturient rutrum erat potenti vestibulum vestibulum placerat netus convallis dignissim sapien pulvinar semper leo consectetuer nulla elementum sociis pellentesque ullamcorper pretium sagittis mus facilisis nec rhoncus suspendisse eros nibh fermentum taciti porta sollicitudin adipiscing" },
						new Place() { Name="Aliquet sollicitudin", Image=new BitmapImage(new Uri("ms-appx:/SampleData/SampleUsers/SampleUsers_Files/place_00010.jpg")), Visited="August 23, 2003", Location="2345 22nd St NE, Miami, FL 98052", Description="Condimentum senectus tellus non scelerisque ullamcorper purus parturient per risus tempor sociosqu sodales suscipit fringilla sed nisi viverra aliquam nisl pellentesque nunc tempus torquent tortor velit suspendisse integer sem sit turpis vel ultrices vehicula odio quisque consectetuer volutpat vivamus nam habitasse hendrerit maecenas vestibulum vitae praesent sed varius aliquam orci aliquet pellentesque condimentum scelerisque ullamcorper vestibulum class adipiscing parturient himenaeos dis suspendisse imperdiet sollicitudin pellentesque aenean consectetuer condimentum blandit curae pede malesuada pellentesque commodo accumsan donec penatibus porttitor mauris vestibulum etiam conubia sollicitudin pellentesque bibendum cubilia fusce vestibulum" },
						new Place() { Name="Fermentum", Image=new BitmapImage(new Uri("ms-appx:/SampleData/SampleUsers/SampleUsers_Files/place_00011.jpg")), Visited="May 27, 2007", Location="4567 Main St., Buffalo, NY 98052", Description="Adipiscing dapibus lorem quam scelerisque ullamcorper consectetuer suspendisse est dictumst parturient pellentesque eleifend quis sollicitudin morbi hac condimentum pellentesque nullam aptent tincidunt leo tristique egestas consectetuer auctor facilisi scelerisque vestibulum euismod faucibus mus nulla urna pellentesque nec sollicitudin vestibulum non habitant ultricies proin cras ullamcorper feugiat augue per inceptos venenatis pellentesque adipiscing gravida consectetuer iaculis duis congue vulputate parturient suspendisse interdum lacinia pellentesque dolor nunc fames condimentum cursus sed sollicitudin felis sem sit pellentesque scelerisque curabitur laoreet consectetuer vestibulum pellentesque vestibulum ipsum vel justo ullamcorper phasellus nam amet suspendisse sollicitudin consequat natoque" }
					}
				},
				new User()
				{
					FullName="Argentiero, Luca",
					Email="user-8@fabrikam.com",
					ProfileBackground=new BitmapImage(new Uri("ms-appx:/SampleData/SampleUsers/SampleUsers_Files/gradient_005.jpg")),
					Address="4567 1st Ave, San Francisco, CA 99999",
					PhoneNumber="(222) 555-0121",
					MemberSince="February 25, 2008",
					IsActive=false,
					Company="Margie's Travel",
					ProfilePic=new BitmapImage(new Uri("ms-appx:/SampleData/SampleUsers/SampleUsers_Files/profile_0019.jpg")),
					Places = new List<Place>()
					{
						new Place() { Name="Blandit faucibus", Image=new BitmapImage(new Uri("ms-appx:/SampleData/SampleUsers/SampleUsers_Files/place_00020.jpg")), Visited="May 24, 2006", Location="1234 Main St., Buffalo, NY 98052", Description="Lacus condimentum lobortis sed adipiscing pellentesque consectetuer convallis nascetur scelerisque dis pharetra posuere ullamcorper parturient placerat est vestibulum vestibulum dictum suspendisse adipiscing ante lorem lectus parturient potenti pretium arcu vestibulum pulvinar hac libero condimentum scelerisque diam rhoncus vestibulum pellentesque sodales sollicitudin eget viverra ullamcorper elit aliquam adipiscing pellentesque suspendisse sagittis leo mus magna massa senectus integer parturient metus vestibulum consectetuer" },
						new Place() { Name="Fringilla eget", Image=new BitmapImage(new Uri("ms-appx:/SampleData/SampleUsers/SampleUsers_Files/place_00010.jpg")), Visited="September 19, 2004", Location="2345 Main St., Buffalo, NY 98052", Description="Sociosqu quisque vivamus condimentum ligula nec morbi dignissim pellentesque suscipit non neque scelerisque netus aliquam vestibulum litora nulla per sollicitudin pellentesque aliquet torquent elementum facilisis ultrices adipiscing sed porta luctus magnis enim sem consectetuer pellentesque blandit fermentum vehicula volutpat erat purus eros sit mattis commodo ullamcorper nibh vel suspendisse condimentum nisi conubia nisl parturient sollicitudin nunc scelerisque ullamcorper risus suspendisse velit odio mauris fringilla vitae condimentum class pellentesque mollis consectetuer nam vestibulum vestibulum scelerisque adipiscing ullamcorper cubilia curae orci suspendisse montes habitasse donec nostra etiam pellentesque sed fusce sollicitudin parturient maecenas condimentum lorem vestibulum pellentesque dis vestibulum ornare est" },
						new Place() { Name="Suspendisse", Image=new BitmapImage(new Uri("ms-appx:/SampleData/SampleUsers/SampleUsers_Files/place_00011.jpg")), Visited="August 10, 2003", Location="789 Main St., Buffalo, NY 98052", Description="Praesent accumsan pede adipiscing scelerisque ullamcorper platea parturient morbi hac leo vestibulum bibendum hendrerit dapibus egestas dictumst consectetuer pellentesque primis nulla suspendisse sollicitudin pellentesque eleifend rutrum quam euismod mus consectetuer himenaeos quis nec proin facilisi augue urna pellentesque sapien faucibus condimentum cras vestibulum dolor scelerisque non sollicitudin duis ullamcorper fames semper per adipiscing sed suspendisse parturient habitant nunc amet inceptos imperdiet pellentesque vestibulum felis sociis consectetuer vestibulum taciti sem ipsum justo interdum malesuada feugiat lobortis sit vel adipiscing ante nascetur tellus pellentesque penatibus parturient porttitor pharetra gravida sollicitudin tempor condimentum tincidunt nam scelerisque ullamcorper pellentesque consectetuer iaculis placerat suspendisse" },
						new Place() { Name="Habitasse mus", Image=new BitmapImage(new Uri("ms-appx:/SampleData/SampleUsers/SampleUsers_Files/place_00012.jpg")), Visited="September 20, 2007", Location="1234 Front St., Seattle, WA 12345", Description="Pellentesque lacus pulvinar sed sagittis dis vestibulum lacinia lorem laoreet vestibulum tristique condimentum est senectus arcu ultricies magna sollicitudin sociosqu adipiscing suscipit parturient hac massa torquent metus vestibulum vestibulum ultrices venenatis vehicula volutpat scelerisque morbi neque ullamcorper suspendisse diam adipiscing pellentesque eget elit maecenas vulputate netus parturient consectetuer tempus curabitur nulla vestibulum praesent pellentesque enim natoque accumsan bibendum condimentum leo vestibulum mus phasellus adipiscing scelerisque erat eros nec ullamcorper porta" },
						new Place() { Name="Vestibulum", Image=new BitmapImage(new Uri("ms-appx:/SampleData/SampleUsers/SampleUsers_Files/place_00013.jpg")), Visited="August 9, 2006", Location="2345 Front St., Seattle, WA 12345", Description="Tortor parturient nibh purus consequat convallis turpis nisi varius sollicitudin suspendisse aenean mauris nisl risus pellentesque non dictumst dignissim vestibulum vestibulum velit per nunc consectetuer condimentum posuere nullam adipiscing eleifend odio elementum sed sem aptent potenti facilisi vitae auctor pretium sit congue class pellentesque orci rhoncus facilisis faucibus sodales scelerisque vel habitant cursus nam inceptos sed parturient dis dictum vestibulum curae sollicitudin viverra vestibulum ullamcorper pede donec lectus adipiscing suspendisse libero condimentum etiam quam parturient ligula pellentesque est" }
					}
				},
				new User()
				{
					FullName="Perry, Brian",
					Email="someone-1@adventure-works.com",
					ProfileBackground=new BitmapImage(new Uri("ms-appx:/SampleData/SampleUsers/SampleUsers_Files/gradient_006.jpg")),
					Address="1234 Main St., San Francisco, CA 99999",
					PhoneNumber="(333) 555-0122",
					MemberSince="July 20, 2008",
					IsActive=true,
					Company="Northwind Traders",
					ProfilePic=new BitmapImage(new Uri("ms-appx:/SampleData/SampleUsers/SampleUsers_Files/profile_002.jpg")),
					Places = new List<Place>()
					{
						new Place() { Name="Pellentesque", Image=new BitmapImage(new Uri("ms-appx:/SampleData/SampleUsers/SampleUsers_Files/place_00011.jpg")), Visited="February 25, 2008", Location="789 Front St., Seattle, WA 12345", Description="Consectetuer aliquam fusce scelerisque ullamcorper suspendisse integer fermentum lorem hac quisque morbi fringilla interdum pellentesque lobortis nulla leo sollicitudin vivamus quis mus proin augue condimentum habitasse nascetur dolor fames urna litora cras duis vestibulum nec pellentesque pharetra aliquam nunc placerat luctus amet scelerisque felis ullamcorper aliquet ante vestibulum suspendisse adipiscing consectetuer hendrerit blandit pulvinar pellentesque condimentum arcu" },
						new Place() { Name="Augue", Image=new BitmapImage(new Uri("ms-appx:/SampleData/SampleUsers/SampleUsers_Files/place_00012.jpg")), Visited="July 20, 2008", Location="4567 Main St., Seattle, WA 12345", Description="Sollicitudin pellentesque ipsum magnis justo mattis diam parturient mauris eget consectetuer sagittis non per scelerisque pellentesque mollis elit sollicitudin lacus sed commodo montes conubia vestibulum nostra senectus lorem himenaeos vestibulum sociosqu adipiscing magna parturient suscipit cubilia ullamcorper massa sem vestibulum vestibulum torquent ultrices ornare metus morbi suspendisse adipiscing enim parturient erat platea pellentesque imperdiet eros vehicula malesuada condimentum volutpat scelerisque ullamcorper suspendisse sit penatibus maecenas nibh neque netus dapibus primis nisi nisl vel nunc egestas praesent condimentum nam nulla accumsan euismod vestibulum sed scelerisque" },
						new Place() { Name="Nullam consectetuer", Image=new BitmapImage(new Uri("ms-appx:/SampleData/SampleUsers/SampleUsers_Files/place_00013.jpg")), Visited="September 10, 2006", Location="1234 1st Ave, San Francisco, CA 99999", Description="Bibendum odio ullamcorper dictumst eleifend dis suspendisse est porttitor orci condimentum scelerisque feugiat consectetuer vestibulum hac pellentesque facilisi porta adipiscing tincidunt sollicitudin rutrum parturient pede tristique ultricies venenatis faucibus leo gravida mus vestibulum purus nec sapien semper vestibulum adipiscing sociis iaculis quam habitant ullamcorper risus vulputate quis pellentesque velit vitae inceptos" },
						new Place() { Name="Pellentesque hendrerit", Image=new BitmapImage(new Uri("ms-appx:/SampleData/SampleUsers/SampleUsers_Files/place_00014.jpg")), Visited="September 5, 2005", Location="2345 1st Ave, San Francisco, CA 99999", Description="Suspendisse interdum class curae curabitur condimentum non taciti consectetuer lacinia donec laoreet lobortis parturient scelerisque per urna pellentesque tellus phasellus etiam ullamcorper sollicitudin tempor nascetur natoque posuere pharetra placerat potenti consequat tempus tortor pulvinar sagittis senectus pretium pellentesque turpis sociosqu varius aenean vestibulum consectetuer fusce convallis sed sem rhoncus suscipit pellentesque sit suspendisse dignissim mauris vel elementum sollicitudin vestibulum lorem nullam torquent condimentum sodales adipiscing scelerisque nam sed morbi nulla facilisis cras ultrices ullamcorper proin suspendisse dis pellentesque" },
						new Place() { Name="Commodo", Image=new BitmapImage(new Uri("ms-appx:/SampleData/SampleUsers/SampleUsers_Files/place_00015.jpg")), Visited="December 11, 2007", Location="789 1st Ave, San Francisco, CA 99999", Description="Est condimentum viverra aptent hac consectetuer vehicula fermentum volutpat parturient duis aliquam pellentesque integer augue nunc amet scelerisque quisque dolor ante auctor fringilla sollicitudin ullamcorper fames pellentesque vestibulum vivamus consectetuer congue aliquam pellentesque habitasse sollicitudin pellentesque suspendisse leo aliquet condimentum consectetuer mus cursus dictum scelerisque ullamcorper felis arcu pellentesque blandit" }
					}
				},
				new User()
				{
					FullName="Ramos, Luciana",
					Email="someone-2@adventure-works.com",
					ProfileBackground=new BitmapImage(new Uri("ms-appx:/SampleData/SampleUsers/SampleUsers_Files/gradient_007.jpg")),
					Address ="2345 Front St., San Francisco, CA 99999",
					PhoneNumber="(444) 555-0123",
					MemberSince="September 10, 2006",
					IsActive=false,
					Company="Proseware, Inc.",
					ProfilePic= new BitmapImage(new Uri("ms-appx:/SampleData/SampleUsers/SampleUsers_Files/profile_0020.jpg")),
					Places = new List<Place>()
					{
						new Place() { Name="Condimentum", Image=new BitmapImage(new Uri("ms-appx:/SampleData/SampleUsers/SampleUsers_Files/place_00013.jpg")), Visited="September 26, 2003", Location="4567 1st Ave, San Francisco, CA 99999", Description="Vestibulum lectus libero adipiscing suspendisse hendrerit diam commodo maecenas parturient nec vestibulum ipsum praesent ligula vestibulum conubia litora accumsan luctus magnis eget mattis bibendum mauris non per justo elit sollicitudin sed adipiscing condimentum sem lacus pellentesque sit enim scelerisque lorem parturient cubilia dapibus himenaeos ullamcorper erat suspendisse vestibulum consectetuer vel mollis pellentesque dictumst eleifend" },
						new Place() { Name="Nec", Image=new BitmapImage(new Uri("ms-appx:/SampleData/SampleUsers/SampleUsers_Files/place_00014.jpg")), Visited="April 24, 2001", Location="1234 Main St., San Francisco, CA 99999", Description="Nam facilisi sollicitudin condimentum egestas vestibulum imperdiet pellentesque magna faucibus adipiscing massa consectetuer eros montes euismod malesuada nostra habitant ornare penatibus pellentesque porttitor tincidunt parturient nibh nisi platea primis nisl tristique nunc metus morbi sollicitudin ultricies pellentesque neque vestibulum sed venenatis scelerisque odio ullamcorper consectetuer vulputate orci vestibulum rutrum inceptos feugiat dis sapien netus pellentesque pede suspendisse nulla quam porta interdum purus lobortis quis condimentum curabitur est adipiscing risus phasellus gravida iaculis nascetur lacinia consequat scelerisque parturient ullamcorper" },
						new Place() { Name="Scelerisque adipiscing", Image=new BitmapImage(new Uri("ms-appx:/SampleData/SampleUsers/SampleUsers_Files/place_00015.jpg")), Visited="September 30, 2005", Location="2345 Front St., San Francisco, CA 99999", Description="Vestibulum urna hac laoreet sollicitudin suspendisse vestibulum convallis adipiscing pellentesque semper parturient condimentum dignissim cras pharetra natoque sociis velit leo consectetuer vitae placerat taciti pulvinar vestibulum mus pellentesque posuere scelerisque elementum nec potenti non sagittis sollicitudin class duis curae vestibulum pretium donec pellentesque nunc rhoncus sodales ullamcorper adipiscing etiam per sed tellus senectus facilisis suspendisse sociosqu suscipit fermentum consectetuer fusce parturient vestibulum amet condimentum lorem morbi tempor vestibulum ante arcu nulla tempus sem torquent fringilla adipiscing tortor" },
						new Place() { Name="Conubia cubilia", Image=new BitmapImage(new Uri("ms-appx:/SampleData/SampleUsers/SampleUsers_Files/place_00016.jpg")), Visited="April 7, 2001", Location="789 22nd St NE, Miami, FL 98052", Description="Viverra pellentesque sit scelerisque proin habitasse turpis ullamcorper varius aliquam parturient augue vel integer ultrices diam nam sollicitudin hendrerit sed dis suspendisse dolor eget elit himenaeos enim vestibulum vestibulum fames est pellentesque hac erat leo adipiscing condimentum aenean vehicula mus parturient nec consectetuer vestibulum volutpat quisque eros imperdiet vestibulum nibh non malesuada pellentesque adipiscing felis penatibus porttitor sollicitudin parturient maecenas praesent tincidunt nisi per" },
						new Place() { Name="Non", Image=new BitmapImage(new Uri("ms-appx:/SampleData/SampleUsers/SampleUsers_Files/place_00017.jpg")), Visited="February 25, 2007", Location="4567 22nd St NE, Miami, FL 98052", Description="Vivamus vestibulum ipsum scelerisque mauris ullamcorper justo tristique nullam ultricies lacus sed suspendisse accumsan bibendum venenatis dictumst aliquam sem eleifend nisl aliquet pellentesque aptent condimentum blandit sit consectetuer facilisi pellentesque commodo nunc sollicitudin pellentesque lorem vestibulum magna massa metus scelerisque ullamcorper auctor morbi congue conubia neque adipiscing odio cursus orci vel nam vulputate faucibus suspendisse" }
					}
				}
			});
		}
	}

	[Windows.UI.Xaml.Data.Bindable]
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

	[Windows.UI.Xaml.Data.Bindable]
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