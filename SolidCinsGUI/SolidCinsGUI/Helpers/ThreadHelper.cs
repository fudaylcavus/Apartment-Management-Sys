using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidCinsGUI.Helpers
{

    public static class ThreadHelper
    {
        delegate void SetCallbackFormOnly(Form f);
        delegate void SetCallback(Form f, Control ctrl, string text);
        public static void SetText(Form form, Control ctrl, string text)
        {
            if (form == null)
            {
                return;
            }

            if (ctrl.InvokeRequired)
            {
                SetCallback d = new SetCallback(SetText);
                form.Invoke(d, new object[] { form, ctrl, text });
            }
            else
            {
                ctrl.Text = text;
            }
        }

        public static void RemoveItem(Form form, Control ctrl, string item)
        {
            if (form == null)
            {
                return;
            }
            if (ctrl.InvokeRequired)
            {
                SetCallback d = new SetCallback(RemoveItem);
                form.Invoke(d, new object[] { form, ctrl, item });
            }
            else
            {
                ((ListBox)ctrl).Items.Remove(item);
            }

        }

        public static void ShowForm(Form form)
        {
            if (form == null)
            {
                return;
            }
            LoginForm.Instance.Invoke((MethodInvoker)delegate ()
            {
                form.Show();
            });


        }


        public static void HideForm(Form form)
        {
            if (form == null)
            {
                return;
            }

            LoginForm.Instance.Invoke((MethodInvoker)delegate ()
            {
                form.Hide();
            });



        }




        public static void AddItem(Form form, Control ctrl, string item)
        {
            if (form == null)
            {
                return;
            }
            if (ctrl.InvokeRequired)
            {
                SetCallback d = new SetCallback(AddItem);
                form.Invoke(d, new object[] { form, ctrl, item });
            }
            else
            {
                ((ListBox)ctrl).Items.Add(item);

                //Scroll to bottom
                ((ListBox)ctrl).TopIndex = ((ListBox)ctrl).Items.Count - 1;
            }
        }


    }
}
