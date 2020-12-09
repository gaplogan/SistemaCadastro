using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaCadastro
{
    public partial class Form1 : Form
    {
        List<Pessoa> pessoas;

        public Form1()
        {
            InitializeComponent();

            pessoas = new List<Pessoa>();

            comboEC.Items.Add("Casado");
            comboEC.Items.Add("Solteiro");
            comboEC.Items.Add("Viuvo");
            comboEC.Items.Add("Separado");
            comboEC.SelectedIndex = 0;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            #region Preenchimento da lista de contatos
            List<string> tels = new List<string>();
            tels.Add("12988777624");
            tels.Add("13988786794");
            pessoas.Add(new Pessoa()
            {
                Nome = "Gabriel",
                DataNascimento = "10/01/1985",
                EstadoCivil = "Casado",
                Telefone = tels,
                CasaPropria = true,
                Veiculo = true,
                Sexo = 'M'
            });

            List<string> tels2 = new List<string>();
            tels2.Add("12988420461");
            pessoas.Add(new Pessoa()
            {
                Nome = "Danny",
                DataNascimento = "27/02/1986",
                EstadoCivil = "Casado",
                Telefone = tels2,
                CasaPropria = true,
                Veiculo = false,
                Sexo = 'F'
            });

            List<string> tels3 = new List<string>();
            tels3.Add("13945721526");
            tels3.Add("12988457451");
            tels3.Add("12988647512");
            pessoas.Add(new Pessoa()
            {
                Nome = "Arthur",
                DataNascimento = "17/02/2011",
                EstadoCivil = "Solteiro",
                Telefone = tels3,
                CasaPropria = false,
                Veiculo = false,
                Sexo = 'M'
            });

            #endregion

            Listar();
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            int index = -1;

            foreach (Pessoa pessoa in pessoas)
            {
                if (pessoa.Nome == txtNome.Text)
                {
                    index = pessoas.IndexOf(pessoa);
                }
            }

            if (txtNome.Text == "")
            {
                MessageBox.Show("Preencha o nome");
                txtNome.Focus();
                return;
            }

            if (txtTelefone.Text == "")
            {
                MessageBox.Show("Preencha o telefone");
                txtTelefone.Focus();
                return;
            }

            char sexo;

            if (radioM.Checked)
            {
                sexo = 'M';
            }
            else if (radioF.Checked)
            {
                sexo = 'F';
            }
            else
            {
                sexo = 'O';
            }

            Pessoa p = new Pessoa();
            p.Nome = txtNome.Text;
            p.DataNascimento = txtData.Text;
            p.EstadoCivil = comboEC.SelectedItem.ToString();
            // Alterar        

            p.Telefone.Add(txtTelefone.Text);
            p.CasaPropria = checkCasa.Checked;
            p.Veiculo = checkCarro.Checked;
            p.Sexo = sexo;

            if (index < 0)
            {
                pessoas.Add(p);
            }
            else
            {
                pessoas[index] = p;
            }

            btnLimpar_Click(btnLimpar, EventArgs.Empty);

            Listar();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            int indice = lista.SelectedIndex;

            pessoas.RemoveAt(indice);

            Listar();
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            txtNome.Text = "";
            txtData.Text = "";
            comboEC.SelectedIndex = 0;
            txtTelefone.Text = "";
            checkCasa.Checked = false;
            checkCarro.Checked = false;
            radioM.Checked = true;
            radioF.Checked = false;
            radioO.Checked = false;
            txtNome.Focus();
        }

        public void Listar()
        {
            lista.Items.Clear();

            foreach (Pessoa p in pessoas)
            {
                lista.Items.Add(p.Nome);
            }
        }

        private void lista_MouseDoubleClick(object sender, MouseEventArgs e)
        {            
            Pessoa p = pessoas[lista.SelectedIndex];

            txtNome.Text = p.Nome;
            txtData.Text = p.DataNascimento;
            comboEC.SelectedItem = p.EstadoCivil;            
            //////////////////////////////////
            int posY = 0;

            foreach (Control c in Controls)
            {
                if (c is MaskedTextBox)
                {
                    Controls.Remove(c);
                    this.Size = new Size(816,692);
                }
            }

            for (int i = 0; i < p.Telefone.Count; i++)
            {
                if (i < 1)
                {
                    MaskedTextBox txtTel = new MaskedTextBox();
                    txtTel.Mask = "(00) 00000-0000";
                    txtTel.Size = new Size(121, 20);
                    txtTel.Location = new Point(111, 130);
                    this.Size = new Size(Width, Height);
                    Controls.Add(txtTel);
                    txtTel.Text = p.Telefone[i];
                }
                else
                {
                    posY += 25;
                    MaskedTextBox txtTel = new MaskedTextBox();
                    txtTel.Mask = "(00) 00000-0000";
                    txtTel.Size = new Size(121, 20);
                    txtTel.Location = new Point(111, 130 + posY);
                    this.Size = new Size(Width, Height + 25);
                    Controls.Add(txtTel);
                    txtTel.Text = p.Telefone[i];
                }
            }
            
            //////////////////////////////////
            checkCasa.Checked = p.CasaPropria;
            checkCarro.Checked = p.Veiculo;

            switch (p.Sexo)
            {
                case 'M':
                    radioM.Checked = true;
                    break;
                case 'F':
                    radioF.Checked = true;
                    break;
                default:
                    radioO.Checked = true;
                    break;
            }
        }

        private void btnAddTel_Click(object sender, EventArgs e)
        {
            int posY = 0;

            foreach (var c in Controls)
            {
                if (c is MaskedTextBox)
                {
                    posY += 25;
                    //((MaskedTextBox)c).Text = "";
                }
            }

            MaskedTextBox txtTelefone = new MaskedTextBox();            
            txtTelefone.Mask = "(00) 00000-0000";
            txtTelefone.Size = new Size(121, 20);
            txtTelefone.Location = new Point(111, 130 + posY);
            this.Size = new Size(Width, Height + 25);
            Controls.Add(txtTelefone);            
        }
    }
}
