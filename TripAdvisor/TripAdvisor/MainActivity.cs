using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Content;
using System;
using Android.Content;
using Android.Database;
using Android.Database.Sqlite;

namespace TripAdvisor
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : Activity
    {

        EditText name, pwd;
        Button logBtn, reg;
        Android.App.AlertDialog.Builder myAlert;
        ICursor result;
        DBHelper help;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            name = FindViewById<EditText>(Resource.Id.userName);
            pwd = FindViewById<EditText>(Resource.Id.password);
            logBtn = FindViewById<Button>(Resource.Id.logIn);
            reg = FindViewById<Button>(Resource.Id.signUp);

            logBtn.Click += shwDialog;

            myAlert = new Android.App.AlertDialog.Builder(this);


            reg.Click += delegate
            {
                Intent registerPage = new Intent(this, typeof(Register));

                StartActivity(registerPage);
            };


        }

        private void shwDialog(object sender, EventArgs e)
        {
            var nm = name.Text;
            var pswd = pwd.Text;


            if (nm == " " || nm.Equals(""))

            {
                errorDialog("Username");
            }
            else if (pswd == " " || pswd.Equals(""))
            {
                errorDialog("Password");
            }

            else
            {

                String val = "User name is : " + nm + " Password is " + pswd;

                Console.WriteLine("User name is : " + nm + " Password is " + pswd);
                DBHelper help = new DBHelper(this);

                bool condtion = help.selectMyValues(nm, pswd);

                if (condtion == true)
                {
                    Intent welcomePage = new Intent(this, typeof(Welcome));
                    welcomePage.PutExtra("email", nm);
                    welcomePage.PutExtra("code", pswd);
                    StartActivity(welcomePage);
                }
                else
                {
                    myAlert.SetTitle("Error");
                    myAlert.SetMessage("Wrong userName or Password");
                    myAlert.SetPositiveButton("OK", OkAction);

                    Dialog myDialog = myAlert.Create();
                    myDialog.Show();
                }




            }


        }

        private void CancelAction(object sender, DialogClickEventArgs e)
        {
            System.Console.WriteLine("Cancel button is clicked!!!");
        }

        private void errorDialog(string msg)
        {
            myAlert.SetTitle("Error");
            myAlert.SetMessage("Please enter a " + msg);
            myAlert.SetPositiveButton("OK", OkAction);
            Dialog myDialog = myAlert.Create();
            myDialog.Show();
        }


        private void OkAction(object sender, DialogClickEventArgs e)
        {
            System.Console.WriteLine("Ok button is clicked!!!");
        }
    }
}