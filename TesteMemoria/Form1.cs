using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Jogo_da_Memoria.Model;
using System.Data.OleDb;
using Jogo_da_Memoria.Controle;

namespace Jogo_da_Memoria
{
    public partial class Form1 : Form
    {
        Button btn1, btn2, btn3, btn4;
        ProgressBar pgbar;
        Label lbl;
        TableLayoutPanel tblp;
        TextBox txtb;
        Timer timer1, timer2;
        CtrCarta ctrcarta = new CtrCarta();
        //Cartas[] cartas;


        // firstClicked se refere ao controle do primeiro label que o jogador selecionar,
        // mas estará como "null" se o jogador não tiver selecionado nada.
        Label firstClicked = null;

        // secondClicked se refere ao controle do segundo label "control" que o jogador selecionar, 
        Label secondClicked = null;

        // Este objeto Random embaralha os icones dos quadrados
        Random random = new Random();

        // Cada uma destas letras são icones bacanas na fonte Webdings,
        // cada um aparece duas vezes
        List<Cartas> icons = new List<Cartas>();
       
        

        public Form1()
        {
            InitializeComponent();
            
            // Criação do Painel
            tblp = new TableLayoutPanel();
            tblp.Width = 500;
            tblp.Height = 250;
            tblp.BackColor = Color.CornflowerBlue;
            tblp.ColumnCount = 4;
            tblp.RowCount = 2;
            tblp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tblp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tblp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tblp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tblp.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tblp.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tblp.CellBorderStyle = TableLayoutPanelCellBorderStyle.Inset;
            tblp.Dock = DockStyle.Top;
            tblp.Controls.Add(new Label() { Font = new Font("Webdings", 72), TextAlign = ContentAlignment.MiddleCenter, Dock = DockStyle.Fill }, 0, 0);
            tblp.Controls.Add(new Label() { Font = new Font("Webdings", 72), TextAlign = ContentAlignment.MiddleCenter, Dock = DockStyle.Fill }, 1, 0);
            tblp.Controls.Add(new Label() { Font = new Font("Webdings", 72), TextAlign = ContentAlignment.MiddleCenter, Dock = DockStyle.Fill }, 2, 0);
            tblp.Controls.Add(new Label() { Font = new Font("Webdings", 72), TextAlign = ContentAlignment.MiddleCenter, Dock = DockStyle.Fill }, 3, 0);
            tblp.Controls.Add(new Label() { Font = new Font("Webdings", 72), TextAlign = ContentAlignment.MiddleCenter, Dock = DockStyle.Fill }, 0, 1);
            tblp.Controls.Add(new Label() { Font = new Font("Webdings", 72), TextAlign = ContentAlignment.MiddleCenter, Dock = DockStyle.Fill }, 1, 1);
            tblp.Controls.Add(new Label() { Font = new Font("Webdings", 72), TextAlign = ContentAlignment.MiddleCenter, Dock = DockStyle.Fill }, 2, 1);
            tblp.Controls.Add(new Label() { Font = new Font("Webdings", 72), TextAlign = ContentAlignment.MiddleCenter, Dock = DockStyle.Fill }, 3, 1);
            Controls.Add(tblp);
            tblp.GetControlFromPosition(0, 0).Click += new EventHandler(label_Click);
            tblp.GetControlFromPosition(1, 0).Click += new EventHandler(label_Click);
            tblp.GetControlFromPosition(2, 0).Click += new EventHandler(label_Click);
            tblp.GetControlFromPosition(3, 0).Click += new EventHandler(label_Click);
            tblp.GetControlFromPosition(0, 1).Click += new EventHandler(label_Click);
            tblp.GetControlFromPosition(1, 1).Click += new EventHandler(label_Click);
            tblp.GetControlFromPosition(2, 1).Click += new EventHandler(label_Click);
            tblp.GetControlFromPosition(3, 1).Click += new EventHandler(label_Click);

            AtribuiIcones();

            
            
            
            
            
            
            
            //Criação dos Botões
            btn1 = new Button();
            btn1.Text = "Novo";
            btn1.Dock = DockStyle.Bottom;
            btn1.Click += new EventHandler(btn1_Click);
            Controls.Add(btn1);
            

            btn2 = new Button();
            btn2.Text = "Inicio";
            btn2.Dock = DockStyle.Bottom;
            btn2.Click += new EventHandler(btn2_Click);
            Controls.Add(btn2);

            btn3 = new Button();
            btn3.Text = "Ver";
            btn3.Dock = DockStyle.Bottom;
            btn3.Click += new EventHandler(btn3_Click);
            Controls.Add(btn3);

            btn4 = new Button();
            btn4.Text = "Sair";
            btn4.Dock = DockStyle.Bottom;
            btn4.Click += new EventHandler(btn4_Click);
            Controls.Add(btn4);


            //Criação da Pontuação:
            lbl = new Label();
            lbl.Text = "Pontuação:";
            lbl.Dock = DockStyle.Bottom;
            lbl.TextAlign = ContentAlignment.MiddleCenter;
            Controls.Add(lbl);

            txtb = new TextBox();
            txtb.Text = "0";
            txtb.Dock = DockStyle.Bottom;
            txtb.TextAlign = HorizontalAlignment.Center;
            Controls.Add(txtb);

            //Criação de Progress Bar
            pgbar = new ProgressBar();
            pgbar.Dock = DockStyle.Bottom;
            Controls.Add(pgbar);

            //Criação de Timer
            timer1 = new Timer();
            timer1.Interval = 15;
            timer1.Tick += new EventHandler(timer1_Tick);
            timer2 = new Timer();
            timer2.Interval = 15;
            timer2.Tick += new EventHandler(timer2_Tick);

        }

          
      /// Atribui cada icone da lista a um quadrado aleatório

        private void AtribuiIcones()
        {
            ctrcarta.carrega_carta();
            icons = ctrcarta.lista_carta;
            foreach (Control control in tblp.Controls)
            {
                Label iconLabel = control as Label;
                if (iconLabel != null)
                {
                    // int randomNumber = random.Next(icons.Count);

                    //int rdnNumber = ctrcarta.embaralhar();
                    //iconLabel.Text = Convert.ToString(icons[rdnNumber]);
                    int randomNumber = random.Next(icons.Count);
                    iconLabel.Text = icons[randomNumber].simbolo;
                    iconLabel.ForeColor = iconLabel.BackColor;
                    icons.RemoveAt(randomNumber);
                }

                tblp.GetControlFromPosition(1, 0).Visible = true;
                tblp.GetControlFromPosition(2, 0).Visible = true;
                tblp.GetControlFromPosition(3, 0).Visible = true;
                tblp.GetControlFromPosition(0, 1).Visible = true;
                tblp.GetControlFromPosition(1, 1).Visible = true;
                tblp.GetControlFromPosition(2, 1).Visible = true;
                tblp.GetControlFromPosition(3, 1).Visible = true;
            }

            
        }

        //Metodo para habilitar os labels
        private void Habilitar()
        {

            tblp.GetControlFromPosition(0, 0).Visible = true;
            tblp.GetControlFromPosition(1, 0).Visible = true;
            tblp.GetControlFromPosition(2, 0).Visible = true;
            tblp.GetControlFromPosition(3, 0).Visible = true;
            tblp.GetControlFromPosition(0, 1).Visible = true;
            tblp.GetControlFromPosition(1, 1).Visible = true;
            tblp.GetControlFromPosition(2, 1).Visible = true;
            tblp.GetControlFromPosition(3, 1).Visible = true;
        }

        private void Form1_Load(object sender, EventArgs e)

        {
            txtb.Text = "0";

            //Este item desabilita os labels até o jogador selecionar "Inicio"
            //Trava também os demais botões.
            btn1.Enabled = false;
            btn2.Enabled = true;
            btn3.Enabled = false;
            tblp.GetControlFromPosition(0, 0).Visible = false;
            tblp.GetControlFromPosition(1, 0).Visible = false;
            tblp.GetControlFromPosition(2, 0).Visible = false;
            tblp.GetControlFromPosition(3, 0).Visible = false;
            tblp.GetControlFromPosition(0, 1).Visible = false;
            tblp.GetControlFromPosition(1, 1).Visible = false;
            tblp.GetControlFromPosition(2, 1).Visible = false;
            tblp.GetControlFromPosition(3, 1).Visible = false;          
        }


        /// Este evento é atribuido a todos os labels do formulário
        private void label_Click(object sender, EventArgs e)
        {
            // O timer só irá iniciar depois de um par errado ser mostrado.
            // Então, ignora qualquer clique se o timer estiver iniciado
            if (timer1.Enabled == true)
                return;
            Label clickedLabel = sender as Label;

            if (clickedLabel != null)
            {
                // Se o icone selecionado já tiver sido mostrado, ingnora o clique.
                if (clickedLabel.ForeColor == Color.Black)
                    return;

                // Se o firstClicked estiver como "null", este é o primeiro ícone do jogador.
                // Então, a cor é alterada para "black", e retorna.
                if (firstClicked == null)
                {
                    firstClicked = clickedLabel;
                    firstClicked.ForeColor = Color.Black;

                    return;
                }

                // Se o jogador chegou até aqui, o timer não está ativo e o firstClicked não está como "null"
                // Então, esta será a segunda jogada.
                // A cor será alterada para "black".
                secondClicked = clickedLabel;
                secondClicked.ForeColor = Color.Black;

                // Aqui o jogo verifica se o jogador ganhou, ou se há mais jogadas.
                CheckForWinner();

                // Se o jogador localizou dois itens iguais, eles permanecem "black" e o first e secondClikec são resetados.
                // Permitindo outra jogada.
                if (firstClicked.Text == secondClicked.Text)
                {
                    firstClicked = null;
                    secondClicked = null;
                    txtb.Text = Convert.ToString(Convert.ToInt16(txtb.Text) + 10);

                    return;
                }

                // Se o jogador chegar até aqui, dois icones diferentes foram selecionados
                // Então é iniciado o timer, que vai logo esconder os icones.
                timer1.Start();
            }
        }

        /// Este timer inicia quando o jogador seleciona dois icones diferentes.
        /// Ele se desliga e esconde os dois icones.
        private void timer1_Tick(object sender, EventArgs e)
        {
            pgbar.Value = pgbar.Value + 1;
            // Para o Timer
            //



            if (pgbar.Value == 99)
            {
                pgbar.Value = 0;
                timer1.Stop();
                // Esconde os dois icones
                firstClicked.ForeColor = firstClicked.BackColor;
                secondClicked.ForeColor = secondClicked.BackColor;

                // Reseta o firstClicked e o secondClicked 
                // Sendo assim, o programa sabera que a proxima seleção será a primeira jogada.
                firstClicked = null;
                secondClicked = null;
            }
        }

        /// Este item checa se todos os itens estão revelados, comparando a cor de fundo com a cor da letra.
        /// Se todos os icones estiverem revelados, o jogador ganhou.
        private void CheckForWinner()
        {
            // Checa todos os labels do TableLayoutPanel, 
            // Confirma se todos estão revelados
            foreach (Control control in tblp.Controls)
            {
                Label iconLabel = control as Label;

                if (iconLabel != null)
                {
                    if (iconLabel.ForeColor == iconLabel.BackColor)
                        return;
                }
            }

            // Se o loop não retornar, ele não achou nenhum icone diferente.
            // Isso significa que o jogador ganhou. Ele fecha então a aplicação.
            MessageBox.Show("Você encontrou todas as imagens!", "Parabéns!");
            btn2.Enabled = false;
            btn3.Enabled = false;
            
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            Close();
        }

        //Botão "Novo"
        //Nesta parte, tive alguns problemas para redistribuir os itens pelo próprio código.
        //Com isso, reiniciei a aplicação.
        private void btn1_Click(object sender, EventArgs e)
        {
            Application.Restart();
            
        }

        //Botão "Iniciar"
        private void btn2_Click(object sender, EventArgs e)
        {
            Habilitar();
            btn1.Enabled = true;
            btn3.Enabled = true;
            btn2.Enabled = false;
        }

        //Botão "Ver"
        private void btn3_Click(object sender, EventArgs e)
        {
            timer2.Start();
        }

        //Timer do botão "Ver"
        private void timer2_Tick(object sender, EventArgs e)
        {
            pgbar.Value = pgbar.Value + 1;
            tblp.GetControlFromPosition(0, 0).Enabled = false;
            tblp.GetControlFromPosition(1, 0).Enabled = false;
            tblp.GetControlFromPosition(2, 0).Enabled = false;
            tblp.GetControlFromPosition(3, 0).Enabled = false;
            tblp.GetControlFromPosition(0, 1).Enabled = false;
            tblp.GetControlFromPosition(1, 1).Enabled = false;
            tblp.GetControlFromPosition(2, 1).Enabled = false;
            tblp.GetControlFromPosition(3, 1).Enabled = false;

            if (pgbar.Value == 99)
            {
                pgbar.Value = 0;
                tblp.GetControlFromPosition(0, 0).Enabled = true;
                tblp.GetControlFromPosition(1, 0).Enabled = true;
                tblp.GetControlFromPosition(2, 0).Enabled = true;
                tblp.GetControlFromPosition(3, 0).Enabled = true;
                tblp.GetControlFromPosition(0, 1).Enabled = true;
                tblp.GetControlFromPosition(1, 1).Enabled = true;
                tblp.GetControlFromPosition(2, 1).Enabled = true;
                tblp.GetControlFromPosition(3, 1).Enabled = true;
                btn3.Enabled = false;
                timer2.Stop();

            }
        }
    }
}
