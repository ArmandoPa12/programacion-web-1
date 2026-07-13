namespace Formulario_Win
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar);
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar);
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            string box1 = txtbox1.Text;
            string box2 = txtbox2.Text;

            Console.WriteLine(box1);
            Console.WriteLine(box2);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string palabra = richTextBox1.Text;
            Console.Write(palabra);
        }
    }
}
