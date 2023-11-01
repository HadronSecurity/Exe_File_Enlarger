using System;
using System.IO;
using System.Windows.Forms;

namespace Exe_File_Enlarger
{
    public partial class File_Pumper : Form
    {
        private string secilenExeYolu;
        public File_Pumper()
        {
            InitializeComponent();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Exe Dosyaları (*.exe)|*.exe";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                secilenExeYolu = openFileDialog.FileName;
                guna2TextBox1.Text = secilenExeYolu;
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(secilenExeYolu))
            {
                MessageBox.Show("Lütfen bir exe dosyası seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            long boyutArtisi = 0;

            if (MB.Checked)
            {
                boyutArtisi = (long)guna2NumericUpDown1.Value * 1024 * 1024; // MB to Byte
            }
            else if (GB.Checked)
            {
                boyutArtisi = (long)guna2NumericUpDown1.Value * 1024 * 1024 * 1024; // GB to Byte
            }
            else if (TB.Checked)
            {
                boyutArtisi = (long)guna2NumericUpDown1.Value * 1024 * 1024 * 1024 * 1024; // TB to Byte
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Exe Dosyaları (*.exe)|*.exe";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (FileStream dosya = new FileStream(secilenExeYolu, FileMode.Open))
                    {
                        dosya.Seek(0, SeekOrigin.End);
                        for (long i = 0; i < boyutArtisi; i++)
                        {
                            dosya.WriteByte(0); // Byte'ları sıfırlar, boyutu artırır.
                        }
                    }

                    File.Copy(secilenExeYolu, saveFileDialog.FileName, true); // Seçilen yere yeni dosyayı kaydet (true, varolan dosyayı üzerine yazması için).
                    MessageBox.Show("Dosya boyutu başarıyla değiştirildi ve kaydedildi.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void File_Pumper_Load(object sender, EventArgs e)
        {

        }
    }
}
