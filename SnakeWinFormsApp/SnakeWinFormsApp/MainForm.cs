using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;
using Timer = System.Windows.Forms.Timer;

namespace SnakeWinFormsApp
{
    public partial class MainForm : Form
    {
        private int _mapWidth;
        private int _mapHeight;
        private int _cellsCountWidth;
        private int _cellsCountHeight;
        private int _snakeBodyPieceSize;
        private List<PictureBox> snakeBody;
        private int newHeadLocationX;
        private int newHeadLocationY;
        private int moveSideX;
        private int moveSideY;
        Timer timer;
        Random random = new Random();
        private event EventHandler<BorderIventArg> Touched;
        private MainMenuForm mainMenuForm;
        private bool isMakedOneMove = true;
        private int maxBodySize;
        private PictureBox _head;
        private PictureBox _food;
        private int _previousX;
        private int _previousY;

        public MainForm(MainMenuForm mainMenuForm)
        {
            InitializeComponent();
            this.mainMenuForm = mainMenuForm;        
            Width = 500;
            Height = 500;
            _snakeBodyPieceSize = 30;
            _mapWidth = (Width - startButton.Width) / _snakeBodyPieceSize;
            _mapHeight = Height / _snakeBodyPieceSize;
            _head = snakeHeadPictureBox;
            _head.Size = new Size(_snakeBodyPieceSize, _snakeBodyPieceSize);
            _cellsCountWidth = _mapWidth;
            _cellsCountHeight = _mapHeight;
            newHeadLocationX = (_cellsCountWidth / 2) * _snakeBodyPieceSize;
            newHeadLocationY = (_cellsCountHeight * _snakeBodyPieceSize) / 2;
            maxBodySize = ((_cellsCountWidth - 1) * _cellsCountHeight);
            _head.Location = new Point(newHeadLocationX, newHeadLocationY);
            CreateMap();
            NewGame();
        }
        private void NewGame()
        {
            moveSideX = _snakeBodyPieceSize;
            moveSideY = 0;
            Touched += MainForm_Touched;
            snakeBody = new(maxBodySize - 1);
            _food = new PictureBox();
            startButton.Enabled = true;
            timer = new();
            timer.Interval = 500;
            timer.Tick += Timer_Tick;            
        }
        private void startButton_Click(object sender, EventArgs e)
        {
            timer.Start();
            startButton.Enabled = false;
        }
        private void MainForm_Touched(object? sender, BorderIventArg e)
        {
            switch (e.Side)
            {
                case BorderSide.Left:

                case BorderSide.Right:

                case BorderSide.Top:

                case BorderSide.Bottom:
                    EndGame();
                    break;
            }
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            MoveSnake();
            CreateFood();
            IsEatedFood();
        }
        private PictureBox CreatPieceOFBody(int locationX, int locationY)
        {
            var bodyPiece = new PictureBox();
            bodyPiece.BackColor = Color.Gray;
            bodyPiece.BorderStyle = BorderStyle.FixedSingle;
            bodyPiece.Size = new Size(_snakeBodyPieceSize, _snakeBodyPieceSize);
            bodyPiece.Location = new Point(locationX, locationY);
            return bodyPiece;
        }
        private PictureBox MoveHead(int locationX, int locationY)
        {            
            _head.BorderStyle = BorderStyle.FixedSingle;
            _head.Location = new Point(locationX, locationY);
            return _head;
        }
        private void MoveSnake()
        {
            scoreLabel.Text = snakeBody.Count().ToString();
            _previousX = _head.Location.X;
            _previousY = _head.Location.Y;
            newHeadLocationX += moveSideX;
            newHeadLocationY += moveSideY;
            MoveHead(newHeadLocationX, newHeadLocationY);
            if(snakeBody.Count() > 0)
            {
                Controls.Remove(snakeBody.Last());
                snakeBody.Remove(snakeBody.Last());
                snakeBody.Insert(0, CreatPieceOFBody(_previousX, _previousY));
                Controls.Add(snakeBody.First());
            }                       
            isMakedOneMove = true;
            if (!IsMovedBehindTheBorder())
            {
                IsEndGame();
            }
        }

        private void CreateMap()
        {
            for (int i = 0; i < _mapWidth; i++)
            {
                var line = new PictureBox();
                line.BackColor = Color.Black;
                line.Location= new Point(i * _snakeBodyPieceSize, 0);
                line.Size = new Size(1, _mapHeight * _snakeBodyPieceSize);
                Controls.Add(line);                
            }
            for (int i = 0; i <= _mapHeight; i++)
            {
                var line = new PictureBox();
                line.BackColor = Color.Black;
                line.Location = new Point(0, i * _snakeBodyPieceSize);
                line.Size = new Size((_mapWidth - 1) * _snakeBodyPieceSize, 1);
                Controls.Add(line);
            }
        }
        private void CreateFood()
        {            
            int foodX;
            int foodY;
            if (!_food.Created)
            {
                do
                {
                    foodX = random.Next(0, _cellsCountWidth - 1) * _snakeBodyPieceSize;
                    foodY = random.Next(0, _cellsCountHeight) * _snakeBodyPieceSize;
                    _food.BackColor = Color.Green;
                    _food.BorderStyle = BorderStyle.FixedSingle;
                    _food.Size = new Size(_snakeBodyPieceSize, _snakeBodyPieceSize);
                    _food.Location = new Point(foodX, foodY);
                }
                while (snakeBody.Any(piece => piece.Location.X == foodX && piece.Location.Y == foodY) || _head.Location.X == foodX && _head.Location.Y == foodY);
                Controls.Add(_food);
            }
        }
        private int LeftBorder()
        {
            return 0;
        }
        private int RightBorder()
        {
            return (_mapWidth - 1) * _snakeBodyPieceSize;
        }
        private int TopBorder()
        {
            return 0;
        }
        private int BottomBorder()
        {
            return (_mapHeight - 1) * _snakeBodyPieceSize;
        }
        private bool IsMovedBehindTheBorder()
        {
            if (_head.Location.X < LeftBorder())
            {
                Touched.Invoke(this, new BorderIventArg(BorderSide.Left));
                return true;
            }
            else if (_head.Location.X == RightBorder())
            {
                Touched.Invoke(this, new BorderIventArg(BorderSide.Right));
                return true;
            }
            else if (_head.Location.Y < TopBorder())
            {
                Touched.Invoke(this, new BorderIventArg(BorderSide.Top));
                return true;
            }
            else if (_head.Location.Y > BottomBorder())
            {
                Touched.Invoke(this, new BorderIventArg(BorderSide.Bottom));
                return true;
            }
            return false;
        }
        private void IsEatedFood()
        {
            if (_food.Location.X == _head.Location.X && _food.Location.Y == _head.Location.Y)
            {
                int lastPieceLocationY;
                int lastPieceLocationX;
                if (moveSideX != 0)
                {
                    if(snakeBody.Count != 0)
                    {
                        lastPieceLocationX = snakeBody.Last().Location.X - moveSideX;
                        lastPieceLocationY = snakeBody.Last().Location.Y;
                    }
                    else
                    {
                        lastPieceLocationX = _head.Location.X - moveSideX;
                        lastPieceLocationY = _head.Location.Y;
                    }
                }
                else
                {
                    if (snakeBody.Count != 0)
                    {
                        lastPieceLocationX = snakeBody.Last().Location.X;
                        lastPieceLocationY = snakeBody.Last().Location.Y - moveSideY;
                    }
                    else
                    {
                        lastPieceLocationX = _head.Location.X;
                        lastPieceLocationY = _head.Location.Y - moveSideY;
                    }
                }
                var tail = CreatPieceOFBody(lastPieceLocationX, lastPieceLocationY);
                if(snakeBody.Count != 0)
                {
                    snakeBody.Insert(snakeBody.IndexOf(snakeBody.Last()) + 1, tail);
                }
                else
                {
                    snakeBody.Insert(0, tail);
                }
                Controls.Remove(_food);
                _food = new PictureBox();
            }
        }
        private void IsEndGame()
        {
            if (snakeBody.Count == maxBodySize)
            {
                EndGame();
            }
            else
            {
                for (int i = 0; i < snakeBody.Count; i++)
                {
                    if (_head.Location.X == snakeBody[i].Location.X && _head.Location.Y == snakeBody[i].Location.Y)
                    {
                        EndGame();
                    }
                }
            }
        }

        private void EndGame()
        {
            timer.Enabled = false;
            var score = snakeBody.Count();
            MessageBox.Show($"Игра окончена, ваш результат {score} очка(-ов) из {maxBodySize} очка(-ов)", "Конец игры", MessageBoxButtons.OK, MessageBoxIcon.Information);
            mainMenuForm.newGameButton.Enabled = true;
            Close();
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right && isMakedOneMove)
            {
                if (moveSideY > 0 || moveSideY < 0)
                {
                    moveSideX = _snakeBodyPieceSize;
                    moveSideY = 0;
                }
            }
            else if (e.KeyCode == Keys.Left && isMakedOneMove)
            {
                if (moveSideY > 0 || moveSideY < 0)
                {
                    moveSideX = -_snakeBodyPieceSize;
                    moveSideY = 0;
                }
            }
            else if (e.KeyCode == Keys.Up && isMakedOneMove)
            {
                if (moveSideX > 0 || moveSideX < 0 && isMakedOneMove)
                {
                    moveSideY = -_snakeBodyPieceSize;
                    moveSideX = 0;
                }
            }
            else if (e.KeyCode == Keys.Down && isMakedOneMove)
            {
                if (moveSideX > 0 || moveSideX < 0)
                {
                    moveSideY = _snakeBodyPieceSize;
                    moveSideX = 0;
                }
            }
            isMakedOneMove = false;
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            timer.Stop();
            mainMenuForm.newGameButton.Enabled = true;
        }
    }
}