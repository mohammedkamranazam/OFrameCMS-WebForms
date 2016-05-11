using OWDARO.Helpers;
using OWDARO.OEventArgs;
using OWDARO.Settings;
using ProjectJKL.AppCode.DAL.ShoppingCartModel;
using System;
using System.Web.Security;

namespace OWDARO.UI.UserControls.Components.ShoppingCart
{
    public partial class ProductsCartPopUpComponent : System.Web.UI.UserControl
    {
        public string AddMessageLabelCssClass
        {
            set
            {
                value = " " + value;
                MessageLabel.CssClass += value;
            }
        }

        public BlinkRate BlinkRate
        {
            set
            {
                switch (value)
                {
                    case BlinkRate.Slow:
                        AddMessageLabelCssClass = "Slow";
                        break;

                    case BlinkRate.Regular:
                        AddMessageLabelCssClass = "Regular";
                        break;

                    case BlinkRate.Fast:
                        AddMessageLabelCssClass = "Fast";
                        break;

                    case BlinkRate.None:
                        MessageLabel.CssClass = "MessageLabel";
                        break;
                }
            }
        }

        public string CartPopUpButtonText
        {
            set
            {
                ShowCartPopUpButton.Text = value;
            }
        }

        public bool CartPopUpButtonVisibility
        {
            set
            {
                ShowCartPopUpButton.Visible = value;
            }
        }

        public string Message
        {
            set
            {
                MessageLabel.Text = value;
            }
        }

        public MessageColor MessageColor
        {
            set
            {
                switch (value)
                {
                    case MessageColor.Black:
                        AddMessageLabelCssClass = "Black";
                        break;

                    case MessageColor.Blue:
                        AddMessageLabelCssClass = "Blue";
                        break;

                    case MessageColor.Green:
                        AddMessageLabelCssClass = "Green";
                        break;

                    case MessageColor.Orange:
                        AddMessageLabelCssClass = "Orange";
                        break;

                    case MessageColor.Red:
                        AddMessageLabelCssClass = "Red";
                        break;

                    case MessageColor.White:
                        AddMessageLabelCssClass = "White";
                        break;

                    case MessageColor.Yellow:
                        AddMessageLabelCssClass = "Yellow";
                        break;

                    case MessageColor.None:
                        MessageLabel.CssClass = "MessageLabel";
                        break;
                }
            }
        }

        public bool ShowAddToCartButton
        {
            set
            {
                AddToCartButton.Visible = value;
            }
        }

        public bool ShowShowCartPopUpButton
        {
            set
            {
                ShowCartPopUpButton.Visible = value;
            }
        }

        private void CartGridComponent1_ItemCountChanged(object sender, ItemCountEventArgs e)
        {
            CartPopUpButtonText = string.Format(LanguageHelper.GetKey("ViewCartButtonText"), e.ItemCount);
        }

        private void CartGridComponent1_ShowPopUp(object sender, EventArgs e)
        {
            ShowCartPopUp();
        }

        protected void AddToCartButton_Click(object sender, EventArgs e)
        {
            using (var context = new ShoppingCartEntities())
            {
                var user = Membership.GetUser();

                CartGridComponent1.AddToCart(user, context);
                CartGridComponent1.InitializeCart(user, context);

                ShowCartPopUp();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Visible = AppConfig.ShoppingCartEnabled;

                string locale = CookiesHelper.GetCookie(Constants.Keys.CurrentCultureCookieKey);

                ShowCartPopUpButton.Text = string.Format(LanguageHelper.GetKey("ViewCartButtonText", locale), 0);
                AddToCartButton.Text = LanguageHelper.GetKey("AddToCartButtonText", locale);
            }

            CartGridComponent1.ShowPopUp += CartGridComponent1_ShowPopUp;
            CartGridComponent1.ItemCountChanged += CartGridComponent1_ItemCountChanged;
        }

        public void ShowCartPopUp()
        {
            ShowCartPopUp(string.Empty, BlinkRate.None, MessageColor.None);
        }

        public void ShowCartPopUp(string message, BlinkRate blinkRate, MessageColor messageColor)
        {
            Message = string.Empty;
            BlinkRate = BlinkRate.None;
            MessageColor = MessageColor.None;

            Message = message;
            BlinkRate = blinkRate;
            MessageColor = messageColor;

            ShowCartPopUpButton_ModalPopupExtender.Show();
        }
    }
}