using System;
using System.Windows.Forms;

namespace TaskGroupBy
{
    public delegate void DelBind();
    public partial class frmFields : Form
    {
        public event DelBind EventBind;
        
        public frmFields()
        {
            try
            {
                InitializeComponent();
                txtAmount.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void BindItem()
        {
            try
            {
                cmbItem.DataSource = frmHome.item;
                cmbItem.DisplayMember = "Name";
                cmbItem.ValueMember = "Id";
                cmbItem.SelectedIndex = -1;
            }
            catch
            {
                throw;
            }
        }

        frmHome objHome = new frmHome();
        Logics objLogics = new Logics();
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int qty, itemId;
                DateTime saleDate;
                double rate, amount;

                saleDate = dtpDate.Value;
                itemId = Convert.ToInt32(cmbItem.SelectedValue);
                qty = Convert.ToInt32(txtQty.Text);
                rate = Convert.ToDouble(txtRate.Text);
                amount = Convert.ToDouble(txtAmount.Text);
                objLogics.Insert(saleDate, itemId, qty, rate, amount);

                EventBind.Invoke();
                this.Close();
            }
            catch (FormatException)
            {
                MessageBox.Show("Please Enter Required Fields");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AmountCalc()
        {
            try
            {
                int qty = Convert.ToInt32(txtQty.Text);
                double Rate = Convert.ToDouble(txtRate.Text);
                double Amt = qty * Rate;
                txtAmount.Text = Amt.ToString();
            }
            catch
            {
                throw;
            }
        }

        private void frmFields_Load(object sender, EventArgs e)
        {
            try
            {
                this.BindItem();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtRate_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.AmountCalc();
            }
            catch (FormatException)
            {
                MessageBox.Show("Please Enter Required Fields");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            objLogics.RateKeypress(e);           
        }

        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            objLogics.QtyKeyPress(e);
        }
    }
}
