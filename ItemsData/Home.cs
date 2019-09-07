using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace TaskGroupBy
{

    public partial class frmHome : Form
    {
        public static List<Item> item = new List<Item>();
        public static List<Category> category = new List<Category>();
        public static List<Sale> sale = new List<Sale>();
        Logics objLogics = new Logics();
        double sum = 0;

        public frmHome()
        {
            try
            {
                InitializeComponent();
                this.BindGrid();
                this.TotalAmt();
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

        public void BindGrid()
        {
            try
            {
                dgDisplay.DataSource = objLogics.DisplaySale();
                dgDisplay.RowHeadersVisible = false;
            }
            catch
            {
                throw;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                frmFields objFields = new frmFields();
                objFields.Show();
                objFields.EventBind += ObjFields_EventBind;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                
            }
        }

        private void ObjFields_EventBind()
        {
            try
            {
                this.BindGrid();
                this.TotalAmt();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void frmHome_Load(object sender, EventArgs e)
        {
            try
            {
                Logics objLogics = new Logics();
                item = objLogics.ItemData();
                category = objLogics.CategoryData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void TotalAmt()
        {
            sum = objLogics.AmountSum();
            lblAmt.Text = sum.ToString();
        }

        private void LblAmt_Click(object sender, EventArgs e)
        {

        }
    }
}
