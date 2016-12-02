using Foundation;
using System;
using UIKit;

namespace SmuleClone
{
    public partial class LoginViewController : UIViewController
    {
        public LoginViewController (IntPtr handle) : base (handle)
        {
			
        }

		public override void ViewDidLoad ()  { 	base.ViewDidLoad ();  	var textDelegate = new LoginTextFieldDelegate 	{ 		OnShouldReturn = (sender, textField) => 		{ 			if (textField.Tag == txtUsername.Tag) 			{ 				txtPassword.BecomeFirstResponder(); 			} 			else if (textField.Tag == txtPassword.Tag) 			{ 				txtPassword.ResignFirstResponder();  				// TODO: put your login validation code here 				if (txtPassword.Text == "4995" && 
					txtUsername.Text.ToLower() == "inte") 				{ 					// TODO: navigate to the tabViewController which will in turn call FirstViewController 					TabController tabViewController = this.Storyboard.InstantiateViewController("TabControllerID") as TabController ;  					//Create an instance of our AppDelegate 					var appDelegate = UIApplication.SharedApplication.Delegate as AppDelegate;  					// TODO: set the "Username" property on the FirstViewController here 					appDelegate.Username = txtUsername.Text;  					if (tabViewController != null) 					  PresentViewController(tabViewController,true,null);  				} 				else 				{ 					new UIAlertView("Access Denied!" , "Enter a valid username and password", null,"OK").Show(); 				} 			} 		}  	} ;	
  	txtPassword.Delegate = textDelegate;  	txtUsername.Delegate = textDelegate;  } 

    }
}