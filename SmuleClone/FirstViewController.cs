using System;
using System.Collections.Generic;
using CoreGraphics;
using CoreLocation;
using MapKit;
using UIKit;

namespace SmuleClone
{
	public partial class FirstViewController : UIViewController
	{
		protected FirstViewController(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}

		 // Add a "public" class-level property so other controllers can pass data to this controller 
		public string Username {get;set;}   		// Add the following class-level field because it will be used in several methods  		CLLocationManager locationManager = new CLLocationManager ();   		public override void ViewDidLoad ()  		{ 			base.ViewDidLoad ();  			// Add code for initializing the map here 			// This is only supported on iOS 8 + 			if (UIDevice.CurrentDevice.CheckSystemVersion (8, 0)) { 				locationManager.RequestWhenInUseAuthorization (); 			}  			map.MapType = MKMapType.Standard; 			// map.MapType = MKMapType.Satellite; // this is redundant because we have it set in the storyboard 			// map.MapType = MKMapType.Hybrid;  			map.ShowsUserLocation = true; // this is redundantly set in the storyboard 
			// TODO: show the username of the logged in user. This property was set on the LoginViewController code 			// e.g. "Welcome, John" 			var appDelegate = UIApplication.SharedApplication.Delegate as AppDelegate;  			lblWelcome.Text += " " + appDelegate.Username; 
 			// TODO: Show an annotation based on the selected location
  			SetupPicker();   			SetupSegmentControl ();   			// Set the default annotation: Marquette University  			SetLocationAnnotation ();  		}

		private IList<string> locationList = new List<string>  		{ 			"Marquette University", //43.038702, -87.929728 			"Harley-Davidson Museum", // 43.031892, -87.916508 			"Miller Park", //43.028150, -87.971097 			"Bradley Center" //43.043914, -87.917262 		 };

		private string selectedLocation;  		private void SetLocationAnnotation()  		{ 			CLLocationCoordinate2D geoLocation;  			// based on the selected location, set the Geo-location coordinates 			switch (selectedLocation)  
			{
 			case "Marquette University": 			default: 			geoLocation = new CLLocationCoordinate2D (43.038702, -87.929728); 			break; 			case "Harley-Davidson Museum": 			geoLocation = new CLLocationCoordinate2D (43.031892, -87.916508); 			break; 			case "Miller Park": 			geoLocation = new CLLocationCoordinate2D (43.028150, -87.971097); 			break; 			case "Bradley Center": 			geoLocation = new CLLocationCoordinate2D (43.043914, -87.917262); 			break; 			
			} 				 			// add an annotation 			map.AddAnnotations (new MKPointAnnotation (){ 			// TODO: set the name of the location as the annotation title 			Title= selectedLocation, 			Coordinate = geoLocation, 			} );  			// zoom in and display an area/region within 5 miles  			MKCoordinateSpan span = new MKCoordinateSpan(MilesToLatitudeDegrees(5), MilesToLongitudeDegrees(5, geoLocation.Latitude)); 			map.Region = new MKCoordinateRegion(geoLocation, span);  		}   		// setups up the picker view  		private void SetupPicker()  		{
			// Setup the picker and model 			PickerModel model = new PickerModel(this.locationList); 			model.PickerChanged += (sender, e) => { 			// TODO: set the location to the selected value 			this.selectedLocation = e.SelectedValue;
			}  ;  			UIPickerView picker = new UIPickerView(); 			picker.ShowSelectionIndicator = true; 			picker.Model = model;  			// Setup the toolbar 			UIToolbar toolbar = new UIToolbar(); 			toolbar.BarStyle = UIBarStyle.Default; 			toolbar.Translucent = true; 			toolbar.SizeToFit();  			// Create a 'done' button for the toolbar and add it to the toolbar 			UIBarButtonItem btnDone = new UIBarButtonItem("Done", UIBarButtonItemStyle.Done, (s, e) => { 			this.txtLocation.Text = selectedLocation; 			this.txtLocation.ResignFirstResponder(); 
 			// TODO: Plot the location on the map here 			SetLocationAnnotation(); 			}  ); 			 			toolbar.SetItems(new UIBarButtonItem[]{btnDone}, true);  			// Tell the textbox to use the picker for input 			this.txtLocation.InputView = picker;  			// Display the toolbar over the pickers 			this.txtLocation.InputAccessoryView = toolbar; 		 }   	private void SetupSegmentControl()  	{ 		int typesWidth=260, typesHeight=30, distanceFromBottom=100; 		UISegmentedControl mapTypes = new UISegmentedControl(new CGRect((View.Bounds.Width-typesWidth)/2, View.Bounds.Height-distanceFromBottom, typesWidth, typesHeight)); 		mapTypes.InsertSegment("Road", 0, false); 		mapTypes.InsertSegment("Satellite", 1, false); 		mapTypes.InsertSegment("Hybrid", 2, false); 		mapTypes.SelectedSegment = 0; // Road is the default 		mapTypes.AutoresizingMask = UIViewAutoresizing.FlexibleTopMargin;  		mapTypes.ValueChanged += (s, e) => { 				 		switch(mapTypes.SelectedSegment) { 		case 0: 			map.MapType = MKMapType.Standard; 			break; 		case 1: 			map.MapType = MKMapType.Satellite; 			break; 		case 2: 			map.MapType = MKMapType.Hybrid; 			break; 			}  		} ;  		View.AddSubview(mapTypes);  	}   	/// <summary>Converts miles to latitude degrees</summary>  	public double MilesToLatitudeDegrees(double miles)  	{ 		double earthRadius = 3960.0; // in miles 		double radiansToDegrees = 180.0/Math.PI; 		return (miles/earthRadius) * radiansToDegrees; 	 }  	 /// <summary>Converts miles to longitudinal degrees at a specified latitude</summary>  	public double MilesToLongitudeDegrees(double miles, double atLatitude)  	{ 		double earthRadius = 3960.0; // in miles 		double degreesToRadians = Math.PI/180.0; 		double radiansToDegrees = 180.0/Math.PI; 		// derive the earth's radius at that point in latitude 		double radiusAtLatitude = earthRadius * Math.Cos(atLatitude * degreesToRadians); 		return (miles / radiusAtLatitude) * radiansToDegrees;  	}	
	}
}
