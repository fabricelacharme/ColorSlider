using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ColorSlider
{

    //https://www.codeproject.com/Articles/17395/Owner-drawn-trackbar-slider

    /// <summary>
    /// Encapsulates control that visualy displays certain integer value and allows user to change it within desired range. It imitates <see cref="System.Windows.Forms.TrackBar"/> as far as mouse usage is concerned.
    /// </summary>
    [ToolboxBitmap(typeof(TrackBar))]
    [DefaultEvent("Scroll"), DefaultProperty("BarInnerColor")]
    public partial class ColorSlider : Control
    {
        #region Events

        /// <summary>
        /// Fires when Slider position has changed
        /// </summary>
        [Description("Event fires when the Value property changes")]
        [Category("Action")]
        public event EventHandler ValueChanged;

        /// <summary>
        /// Fires when user scrolls the Slider
        /// </summary>
        [Description("Event fires when the Slider position is changed")]
        [Category("Behavior")]
        public event ScrollEventHandler Scroll;

        #endregion

        #region Properties

        private Rectangle thumbRect; //bounding rectangle of thumb area
        /// <summary>
        /// Gets the thumb rect. Usefull to determine bounding rectangle when creating custom thumb shape.
        /// </summary>
        /// <value>The thumb rect.</value>
        [Browsable(false)]
        public Rectangle ThumbRect
        {
            get { return thumbRect; }
        }

        private Rectangle barRect; //bounding rectangle of bar area
        private Rectangle barHalfRect;
        private Rectangle thumbHalfRect;
        private Rectangle elapsedRect; //bounding rectangle of elapsed area

        private int thumbSize = 16;

        /// <summary>
        /// Gets or sets the size of the thumb.
        /// </summary>
        /// <value>The size of the thumb.</value>
        /// <exception cref="T:System.ArgumentOutOfRangeException">exception thrown when value is lower than zero or grather than half of appropiate dimension</exception>
        [Description("Set Slider thumb size")]
        [Category("ColorSlider")]
        [DefaultValue(16)]
        public int ThumbSize
        {
            get { return thumbSize; }
            set
            {                
                if (value > 0 &
                    value < (barOrientation == Orientation.Horizontal ? ClientRectangle.Width : ClientRectangle.Height))
                    thumbSize = value;
                else
                    throw new ArgumentOutOfRangeException(
                        "TrackSize has to be greather than zero and lower than half of Slider width");                 

                Invalidate();
            }
        }

        private GraphicsPath thumbCustomShape = null;
        /// <summary>
        /// Gets or sets the thumb custom shape. Use ThumbRect property to determine bounding rectangle.
        /// </summary>
        /// <value>The thumb custom shape. null means default shape</value>
        [Description("Set Slider's thumb's custom shape")]
        [Category("ColorSlider")]
        [Browsable(false)]
        [DefaultValue(typeof(GraphicsPath), "null")]
        public GraphicsPath ThumbCustomShape
        {
            get { return thumbCustomShape; }
            set
            {
                thumbCustomShape = value;
                thumbSize = (int) (barOrientation == Orientation.Horizontal ? value.GetBounds().Width : value.GetBounds().Height) + 1;
                Invalidate();
            }
        }

        private Size thumbRoundRectSize = new Size(16, 16);
        /// <summary>
        /// Gets or sets the size of the thumb round rectangle edges.
        /// </summary>
        /// <value>The size of the thumb round rectangle edges.</value>
        [Description("Set Slider's thumb round rect size")]
        [Category("ColorSlider")]
        [DefaultValue(typeof(Size), "16; 16")]
        public Size ThumbRoundRectSize
        {
            get { return thumbRoundRectSize; }
            set
            {
                int h = value.Height, w = value.Width;
                if (h <= 0) h = 1;
                if (w <= 0) w = 1;
                thumbRoundRectSize = new Size(w, h);
                Invalidate();
            }
        }

        private Size borderRoundRectSize = new Size(8, 8);
        /// <summary>
        /// Gets or sets the size of the border round rect.
        /// </summary>
        /// <value>The size of the border round rect.</value>
        [Description("Set Slider's border round rect size")]
        [Category("ColorSlider")]
        [DefaultValue(typeof(Size), "8; 8")]
        public Size BorderRoundRectSize
        {
            get { return borderRoundRectSize; }
            set
            {
                int h = value.Height, w = value.Width;
                if (h <= 0) h = 1;
                if (w <= 0) w = 1;
                borderRoundRectSize = new Size(w, h);
                Invalidate();
            }
        }

        private Orientation barOrientation = Orientation.Horizontal;
        /// <summary>
        /// Gets or sets the orientation of Slider.
        /// </summary>
        /// <value>The orientation.</value>
        [Description("Set Slider orientation")]
        [Category("ColorSlider")]
        [DefaultValue(Orientation.Horizontal)]
        public Orientation Orientation
        {
            get { return barOrientation; }
            set
            {
                if (barOrientation != value)
                {
                    barOrientation = value;
                    int temp = Width;
                    Width = Height;
                    Height = temp;

                    if (thumbCustomShape != null)
                        thumbSize =
                            (int)
                            (barOrientation == Orientation.Horizontal
                                 ? thumbCustomShape.GetBounds().Width
                                 : thumbCustomShape.GetBounds().Height) + 1;
                    Invalidate();
                }
            }
        }


        private int trackerValue = 30;
        /// <summary>
        /// Gets or sets the value of Slider.
        /// </summary>
        /// <value>The value.</value>
        /// <exception cref="T:System.ArgumentOutOfRangeException">exception thrown when value is outside appropriate range (min, max)</exception>
        [Description("Set Slider value")]
        [Category("ColorSlider")]
        [DefaultValue(30)]
        public int Value
        {
            get { return trackerValue; }
            set
            {
                if (value >= _minimum & value <= _maximum)
                {
                    trackerValue = value;
                    if (ValueChanged != null) ValueChanged(this, new EventArgs());
                    Invalidate();
                }
                else throw new ArgumentOutOfRangeException("Value is outside appropriate range (min, max)");
            }
        }


        private int _minimum = 0;
        /// <summary>
        /// Gets or sets the minimum value.
        /// </summary>
        /// <value>The minimum value.</value>
        /// <exception cref="T:System.ArgumentOutOfRangeException">exception thrown when minimal value is greather than maximal one</exception>
        [Description("Set Slider minimal point")]
        [Category("ColorSlider")]
        [DefaultValue(0)]
        public int Minimum
        {
            get { return _minimum; }
            set
            {
                if (value < _maximum)
                {
                    _minimum = value;
                    if (trackerValue < _minimum)
                    {
                        trackerValue = _minimum;
                        if (ValueChanged != null) ValueChanged(this, new EventArgs());
                    }
                    Invalidate();
                }
                else throw new ArgumentOutOfRangeException("Minimal value is greather than maximal one");
            }
        }


        private int _maximum = 100;
        /// <summary>
        /// Gets or sets the maximum value.
        /// </summary>
        /// <value>The maximum value.</value>
        /// <exception cref="T:System.ArgumentOutOfRangeException">exception thrown when maximal value is lower than minimal one</exception>
        [Description("Set Slider maximal point")]
        [Category("ColorSlider")]
        [DefaultValue(100)]
        public int Maximum
        {
            get { return _maximum; }
            set
            {
                if (value > _minimum)
                {
                    _maximum = value;
                    if (trackerValue > _maximum)
                    {
                        trackerValue = _maximum;
                        if (ValueChanged != null) ValueChanged(this, new EventArgs());
                    }
                    Invalidate();
                }
                //else throw new ArgumentOutOfRangeException("Maximal value is lower than minimal one");
            }
        }

        private uint smallChange = 1;
        /// <summary>
        /// Gets or sets trackbar's small change. It affects how to behave when directional keys are pressed
        /// </summary>
        /// <value>The small change value.</value>
        [Description("Set trackbar's small change")]
        [Category("ColorSlider")]
        [DefaultValue(1)]
        public uint SmallChange
        {
            get { return smallChange; }
            set { smallChange = value; }
        }

        private uint largeChange = 5;
        /// <summary>
        /// Gets or sets trackbar's large change. It affects how to behave when PageUp/PageDown keys are pressed
        /// </summary>
        /// <value>The large change value.</value>
        [Description("Set trackbar's large change")]
        [Category("ColorSlider")]
        [DefaultValue(5)]
        public uint LargeChange
        {
            get { return largeChange; }
            set { largeChange = value; }
        }

        private bool drawFocusRectangle = false;
        /// <summary>
        /// Gets or sets a value indicating whether to draw focus rectangle.
        /// </summary>
        /// <value><c>true</c> if focus rectangle should be drawn; otherwise, <c>false</c>.</value>
        [Description("Set whether to draw focus rectangle")]
        [Category("ColorSlider")]
        [DefaultValue(false)]
        public bool DrawFocusRectangle
        {
            get { return drawFocusRectangle; }
            set
            {
                drawFocusRectangle = value;
                Invalidate();
            }
        }

        private bool drawSemitransparentThumb = true;
        /// <summary>
        /// Gets or sets a value indicating whether to draw semitransparent thumb.
        /// </summary>
        /// <value><c>true</c> if semitransparent thumb should be drawn; otherwise, <c>false</c>.</value>
        [Description("Set whether to draw semitransparent thumb")]
        [Category("ColorSlider")]
        [DefaultValue(true)]
        public bool DrawSemitransparentThumb
        {
            get { return drawSemitransparentThumb; }
            set
            {
                drawSemitransparentThumb = value;
                Invalidate();
            }
        }

        private bool mouseEffects = true;
        /// <summary>
        /// Gets or sets whether mouse entry and exit actions have impact on how control look.
        /// </summary>
        /// <value><c>true</c> if mouse entry and exit actions have impact on how control look; otherwise, <c>false</c>.</value>
        [Description("Set whether mouse entry and exit actions have impact on how control look")]
        [Category("ColorSlider")]
        [DefaultValue(true)]
        public bool MouseEffects
        {
            get { return mouseEffects; }
            set
            {
                mouseEffects = value;
                Invalidate();
            }
        }

        private int mouseWheelBarPartitions = 10;
        /// <summary>
        /// Gets or sets the mouse wheel bar partitions.
        /// </summary>
        /// <value>The mouse wheel bar partitions.</value>
        /// <exception cref="T:System.ArgumentOutOfRangeException">exception thrown when value isn't greather than zero</exception>
        [Description("Set to how many parts is bar divided when using mouse wheel")]
        [Category("ColorSlider")]
        [DefaultValue(10)]
        public int MouseWheelBarPartitions
        {
            get { return mouseWheelBarPartitions; }
            set
            {
                if (value > 0)
                    mouseWheelBarPartitions = value;
                else throw new ArgumentOutOfRangeException("MouseWheelBarPartitions has to be greather than zero");
            }
        }


        private Image thumbImage = null;
        /// <summary>
        /// Gets or sets the Image use to render the thumb.
        /// </summary>
        /// <value>the thumb Image</value> 
        [Description("Set to use a specific Image for the thumb")]
        [Category("ColorSlider")]
        [DefaultValue(null)]
        public Image ThumbImage
        {
            get { return thumbImage; }
            set
            {
                if (value != null)                
                    thumbImage = value;                
                else
                    thumbImage = null;
                Invalidate();
            }
        }


        #region colors

        private Color thumbOuterColor = Color.White;
        /// <summary>
        /// Gets or sets the thumb outer color .
        /// </summary>
        /// <value>The thumb outer color.</value>
        [Description("Set Slider thumb outer color")]
        [Category("ColorSlider")]
        [DefaultValue(typeof(Color), "White")]
        public Color ThumbOuterColor
        {
            get { return thumbOuterColor; }
            set
            {
                thumbOuterColor = value;
                Invalidate();
            }
        }


        private Color thumbInnerColor = Color.FromArgb(21, 56, 152);
        /// <summary>
        /// Gets or sets the inner color of the thumb.
        /// </summary>
        /// <value>The inner color of the thumb.</value>
        [Description("Set Slider thumb inner color")]
        [Category("ColorSlider")]        
        public Color ThumbInnerColor
        {
            get { return thumbInnerColor; }
            set
            {
                thumbInnerColor = value;
                Invalidate();
            }
        }


        private Color thumbPenColor = Color.FromArgb(21, 56, 152);
        /// <summary>
        /// Gets or sets the color of the thumb pen.
        /// </summary>
        /// <value>The color of the thumb pen.</value>
        [Description("Set Slider thumb pen color")]
        [Category("ColorSlider")]       
        public Color ThumbPenColor
        {
            get { return thumbPenColor; }
            set
            {
                thumbPenColor = value;
                Invalidate();
            }
        }

    
        private Color barInnerColor = Color.Black;
        /// <summary>
        /// Gets or sets the inner color of the bar.
        /// </summary>
        /// <value>The inner color of the bar.</value>
        [Description("Set Slider bar inner color")]
        [Category("ColorSlider")]
        [DefaultValue(typeof(Color), "Black")]
        public Color BarInnerColor
        {
            get { return barInnerColor; }
            set
            {
                barInnerColor = value;
                Invalidate();
            }
        }
       

        private Color barPenColorElapsedTop = Color.FromArgb(95, 140, 180);   // bleu clair
        [Category("ColorSlider")]
        public Color BarPenColorElapsedTop
        {
            get { return barPenColorElapsedTop; }
            set
            {
                barPenColorElapsedTop = value;
                Invalidate();
            }
        }


        private Color barPenColorElapsedBottom = Color.FromArgb(99, 130, 208);   // bleu très clair
        [Category("ColorSlider")]
        public Color BarPenColorElapsedBottom
        {
            get { return barPenColorElapsedBottom; }
            set
            {
                barPenColorElapsedBottom = value;
                Invalidate();
            }
        }


        private Color barPenColorRemainTop = Color.FromArgb(55, 60, 74);     // gris foncé
        [Category("ColorSlider")]
        public Color BarPenColorTop
        {
            get { return barPenColorRemainTop; }
            set
            {
                barPenColorRemainTop = value;
                Invalidate();
            }
        }

        private Color barPenColorRemainBottom = Color.FromArgb(87, 94, 110);    // gris moyen
        [Category("ColorSlider")]
        public Color BarPenColorBottom
        {
            get { return barPenColorRemainBottom; }
            set
            {
                barPenColorRemainBottom = value;
                Invalidate();
            }
        }   

        private Color elapsedInnerColor = Color.FromArgb(21, 56, 152);
        /// <summary>
        /// Gets or sets the inner color of the elapsed.
        /// </summary>
        /// <value>The inner color of the elapsed.</value>
        [Description("Set Slider's elapsed part inner color")]
        [Category("ColorSlider")]        
        public Color ElapsedInnerColor
        {
            get { return elapsedInnerColor; }
            set
            {
                elapsedInnerColor = value;
                Invalidate();
            }
        }

     
        /// <summary>
        /// Color of graduations
        /// </summary>
        [Description("Color of graduations")]
        [Category("ColorSlider")]    
        private Color tickColor = Color.White;
        public Color TickColor
        {
            get { return tickColor; }
            set
            {
                if (value != tickColor)
                {
                    tickColor = value;
                    Invalidate();
                }
            }
        }

        #endregion


        #region divisions

        private TickStyle tickStyle = TickStyle.TopLeft;
        /// <summary>
        /// Gets or sets where to display the ticks (None, both top-left, bottom-right)
        /// </summary>
        [Description("Gets or sets where to display the ticks")]
        [Category("ColorSlider")]
        [DefaultValue(TickStyle.TopLeft)]
        public TickStyle TickStyle
        {
            get { return tickStyle; }
            set {
                tickStyle = value;
                Invalidate();
            }
        }

        private int _scaleDivisions = 10;
        /// <summary>
        /// How many divisions of maximum?
        /// </summary>
        [Description("Set the number of intervals between minimum and maximum")]
        [Category("ColorSlider")]
        public int ScaleDivisions
        {
            get { return _scaleDivisions; }
            set {
                if (value > 0)
                {
                    _scaleDivisions = value;
                    
                }
                //else throw new ArgumentOutOfRangeException("TickFreqency must be > 0 and < Maximum");

                Invalidate();
                
            }
        }

        private int _scaleSubDivisions = 5;
        /// <summary>
        /// How many subdivisions for each division
        /// </summary>
        [Description("Set the number of subdivisions between main divisions of graduation.")]
        [Category("ColorSlider")]
        public int ScaleSubDivisions
        {
            get { return _scaleSubDivisions; }
            set
            {
                if (value > 0 && _scaleDivisions > 0 && (_maximum - _minimum) / ((value + 1) * _scaleDivisions) > 0)
                { 
                    _scaleSubDivisions = value;
                    
                }
                //else throw new ArgumentOutOfRangeException("TickSubFreqency must be > 0 and < TickFrequency");

                Invalidate();

            }
        }

        private bool _showSmallScale = false;
        /// <summary>
        /// Shows Small Scale marking.
        /// </summary>
        [Description("Show or hide subdivisions of graduations")]
        [Category("ColorSlider")]
        public bool ShowSmallScale
        {
            get { return _showSmallScale; }
            set {

                if (value == true)
                {
                    if (_scaleDivisions > 0 && _scaleSubDivisions > 0 && (_maximum - _minimum) / ((_scaleSubDivisions + 1) * _scaleDivisions) > 0)
                    {
                        _showSmallScale = value;
                        Invalidate();
                    }
                    else
                    {
                        _showSmallScale = false;
                    }
                }
                else
                {
                    _showSmallScale = value;
                    // need to redraw 
                    Invalidate();
                }
            }
        }

        private bool _showDivisionsText = true;
        /// <summary>
        /// Shows Small Scale marking.
        /// </summary>
        [Description("Show or hide text value of graduations")]
        [Category("ColorSlider")]
        public bool ShowDivisionsText
        {
            get { return _showDivisionsText; }
            set { _showDivisionsText = value;
                Invalidate();
            }
        }

        #endregion

        #endregion

        #region Color schemas

        //define own color schemas
        private Color[,] aColorSchema = new Color[,]
            {
                {
                    Color.White,                    // thumb outer
                    Color.FromArgb(21, 56, 152),    // thumb inner
                    Color.FromArgb(21, 56, 152),    // thumb pen color
                    
                    Color.Black,                    // bar inner    

                    Color.FromArgb(95, 140, 180),     // slider elapsed top                   
                    Color.FromArgb(99, 130, 208),     // slider elapsed bottom                    

                    Color.FromArgb(55, 60, 74),     // slider remain top                    
                    Color.FromArgb(87, 94, 110),     // slider remain bottom
                                         
                    Color.FromArgb(21, 56, 152)     // elapsed interieur centre
                },
                {
                    Color.White,                    // thumb outer
                    Color.Red,    // thumb inner
                    Color.Red,    // thumb pen color
                    
                    Color.Black,                    // bar inner    

                    Color.LightCoral,     // slider elapsed top                   
                    Color.Salmon,     // slider elapsed bottom
                    

                    Color.FromArgb(55, 60, 74),     // slider remain top                    
                    Color.FromArgb(87, 94, 110),     // slider remain bottom
                                         
                    Color.Red     // gauche interieur centre
                },
                {
                    Color.White,                    // thumb outer
                    Color.Green,    // thumb inner
                    Color.Green,    // thumb pen color
                    
                    Color.Black,                    // bar inner    

                    Color.SpringGreen,     // slider elapsed top                   
                    Color.LightGreen,     // slider elapsed bottom
                    

                    Color.FromArgb(55, 60, 74),     // slider remain top                    
                    Color.FromArgb(87, 94, 110),     // slider remain bottom
                                         
                    Color.Green     // gauche interieur centre
                },
            };

        public enum ColorSchemas
        {
            BlueColors,
            RedColors,
            GreenColors
        }

        private ColorSchemas colorSchema = ColorSchemas.BlueColors;

        /// <summary>
        /// Sets color schema. Color generalization / fast color changing. Has no effect when slider colors are changed manually after schema was applied. 
        /// </summary>
        /// <value>New color schema value</value>
        [Description("Set Slider color schema. Has no effect when slider colors are changed manually after schema was applied.")]
        [Category("ColorSlider")]
        [DefaultValue(typeof(ColorSchemas), "BlueColors")]
        public ColorSchemas ColorSchema
        {
            get { return colorSchema; }
            set
            {
                colorSchema = value;
                byte sn = (byte)value;
                thumbOuterColor = aColorSchema[sn, 0];
                thumbInnerColor = aColorSchema[sn, 1];
                thumbPenColor = aColorSchema[sn, 2];
                
                barInnerColor = aColorSchema[sn, 3];

                barPenColorElapsedTop = aColorSchema[sn, 4];
                barPenColorElapsedBottom = aColorSchema[sn, 5];

                barPenColorRemainTop = aColorSchema[sn, 6];
                barPenColorRemainBottom = aColorSchema[sn, 7];

                elapsedInnerColor = aColorSchema[sn, 8];

                Invalidate();
            }
        }

        #endregion
        
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ColorSlider"/> class.
        /// </summary>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maximum value.</param>
        /// <param name="value">The current value.</param>
        public ColorSlider(int min, int max, int value)
        {
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw | ControlStyles.Selectable |
                     ControlStyles.SupportsTransparentBackColor | ControlStyles.UserMouse |
                     ControlStyles.UserPaint, true);
            
            // Default backcolor
            BackColor = Color.FromArgb(70, 77, 95);
            // Default image
            ThumbImage = Properties.Resources.BTN_Thumb_Blue;  

            Minimum = min;
            Maximum = max;
            Value = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ColorSlider"/> class.
        /// </summary>
        public ColorSlider() : this(0, 100, 30) { }

        #endregion

        #region Paint

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Paint"></see> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs"></see> that contains the event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            if (!Enabled)
            {
                Color[] desaturatedColors = DesaturateColors(thumbOuterColor, thumbInnerColor, thumbPenColor,
                                                             barInnerColor, 
                                                             barPenColorElapsedTop, barPenColorElapsedBottom, 
                                                             barPenColorRemainTop, barPenColorRemainBottom,
                                                             elapsedInnerColor);
                DrawColorSlider(e, 
                                    desaturatedColors[0], desaturatedColors[1], desaturatedColors[2], 
                                    desaturatedColors[3], 
                                    desaturatedColors[4], desaturatedColors[5], 
                                    desaturatedColors[6], desaturatedColors[7], 
                                    desaturatedColors[8]);
            }
            else
            {
                if (mouseEffects && mouseInRegion)
                {
                    Color[] lightenedColors = LightenColors(thumbOuterColor, thumbInnerColor, thumbPenColor,
                                                            barInnerColor,
                                                            barPenColorElapsedTop, barPenColorElapsedBottom, 
                                                            barPenColorRemainTop, barPenColorRemainBottom,
                                                            elapsedInnerColor);
                    DrawColorSlider(e, 
                        lightenedColors[0], lightenedColors[1], lightenedColors[2], 
                        lightenedColors[3], 
                        lightenedColors[4], lightenedColors[5], 
                        lightenedColors[6], lightenedColors[7], 
                        lightenedColors[8]);
                }
                else
                {
                    DrawColorSlider(e, 
                                    thumbOuterColor, thumbInnerColor, thumbPenColor,
                                    barInnerColor,
                                    barPenColorElapsedTop, barPenColorElapsedBottom, 
                                    barPenColorRemainTop, barPenColorRemainBottom,
                                    elapsedInnerColor);
                }
            }
        }

        /// <summary>
        /// Draws the colorslider control using passed colors.
        /// </summary>
        /// <param name="e">The <see cref="T:System.Windows.Forms.PaintEventArgs"/> instance containing the event data.</param>
        /// <param name="thumbOuterColorPaint">The thumb outer color paint.</param>
        /// <param name="thumbInnerColorPaint">The thumb inner color paint.</param>
        /// <param name="thumbPenColorPaint">The thumb pen color paint.</param>
        /// <param name="barInnerColorPaint">The bar inner color paint.</param>
        /// <param name="barPenColorPaint">The bar pen color paint.</param>
        /// <param name="elapsedInnerColorPaint">The elapsed inner color paint.</param>
        private void DrawColorSlider(PaintEventArgs e, 
            Color thumbOuterColorPaint, Color thumbInnerColorPaint, Color thumbPenColorPaint, 
            Color barInnerColorPaint,
            Color barPenColorPaintElapsedTop, Color barPenColorPaintElapsedBottom, 
            Color barPenColorPaintRemainTop, Color barPenColorPaintRemainBottom,
            Color elapsedInnerColorPaint)
        {
            try
            {
                //set up thumbRect aproprietly
                if (barOrientation == Orientation.Horizontal)
                {
                    #region horizontal
                    if (thumbImage != null)
                    {
                        int TrackX = (((trackerValue - _minimum) * (ClientRectangle.Width - thumbImage.Width)) / (_maximum - _minimum));
                        thumbRect = new Rectangle(TrackX, ClientRectangle.Height/2 - thumbImage.Height/2, thumbImage.Width, thumbImage.Height);
                    }
                    else
                    {
                        int TrackX = (((trackerValue - _minimum) * (ClientRectangle.Width - thumbSize)) / (_maximum - _minimum));
                        thumbRect = new Rectangle(TrackX, 1, thumbSize - 1, ClientRectangle.Height - 3);
                    }
                    #endregion
                }
                else
                {
                    #region vertical
                    if (thumbImage != null)
                    {
                        int TrackY = (((_maximum - (trackerValue - _minimum)) * (ClientRectangle.Height - thumbImage.Height)) / (_maximum - _minimum));
                        thumbRect = new Rectangle(ClientRectangle.Width/2 - thumbImage.Width/2, TrackY, thumbImage.Width, thumbImage.Height);
                    }
                    else
                    {
                        int TrackY = (((_maximum - (trackerValue - _minimum)) * (ClientRectangle.Height - thumbSize)) / (_maximum - _minimum));
                        thumbRect = new Rectangle(1, TrackY, ClientRectangle.Width - 3, thumbSize - 1);
                    }
                    #endregion
                }


                //adjust drawing rects
                barRect = ClientRectangle;
                //barRect = new Rectangle(ClientRectangle.X + 5, ClientRectangle.Y + 5, ClientRectangle.Width - 10, ClientRectangle.Height - 10);
                thumbHalfRect = thumbRect;
                LinearGradientMode gradientOrientation;


                if (barOrientation == Orientation.Horizontal)
                {
                    #region horizontal
                    barRect.Inflate(-1, -barRect.Height / 3);
                    barHalfRect = barRect;
                    barHalfRect.Height /= 2;

                    gradientOrientation = LinearGradientMode.Vertical;
                    

                    thumbHalfRect.Height /= 2;
                    elapsedRect = barRect;
                    elapsedRect.Width = thumbRect.Left + thumbSize / 2;
                    #endregion
                }
                else
                {
                    #region vertical
                    barRect.Inflate(-barRect.Width / 3, -1);
                    barHalfRect = barRect;
                    barHalfRect.Width /= 2;

                   
                    gradientOrientation = LinearGradientMode.Vertical;

                    thumbHalfRect.Width /= 2;
                    elapsedRect = barRect;

                    
                    elapsedRect.Height = barRect.Height - (thumbRect.Top + ThumbSize / 2);

                    elapsedRect.Y = 1 + thumbRect.Top + ThumbSize/2;

                    #endregion
                }
                
                //get thumb shape path 
                GraphicsPath thumbPath;
                if (thumbCustomShape == null)
                    thumbPath = CreateRoundRectPath(thumbRect, thumbRoundRectSize);
                else
                {
                    thumbPath = thumbCustomShape;
                    Matrix m = new Matrix();
                    m.Translate(thumbRect.Left - thumbPath.GetBounds().Left, thumbRect.Top - thumbPath.GetBounds().Top);
                    thumbPath.Transform(m);
                }


                //draw bar

                #region draw inner bar
                // INNER : FAB: draw a single line instead 
                // Dessine une ligne noire sur toute la largeur du controle (elapsed + rest)
                if (barOrientation == Orientation.Horizontal)
                {
                    e.Graphics.DrawLine(new Pen(barInnerColorPaint, 1f), barRect.X, barRect.Y + barRect.Height/2, barRect.X + barRect.Width, barRect.Y + barRect.Height / 2);
                }
                else
                {
                    e.Graphics.DrawLine(new Pen(barInnerColorPaint, 1f), barRect.X + barRect.Width/2, barRect.Y, barRect.X + barRect.Width/2 , barRect.Y + barRect.Height);
                }
                #endregion


                #region draw elapsed bar
                //draw elapsed bar
                // Dessine la partie écoulée (elapsed)


                // FAB: replace by single line
                if (barOrientation == Orientation.Horizontal)
                {
                    e.Graphics.DrawLine(new Pen(elapsedInnerColorPaint, 1f), barRect.X, barRect.Y + barRect.Height / 2, barRect.X + elapsedRect.Width, barRect.Y + barRect.Height / 2);
                }
                else
                {
                    e.Graphics.DrawLine(new Pen(elapsedInnerColorPaint, 1f), barRect.X + barRect.Width / 2, barRect.Y + (barRect.Height - elapsedRect.Height), barRect.X + barRect.Width / 2, barRect.Y + barRect.Height);
                }

                #endregion draw elapsed bar


                #region draw external contours
                // -----------------------------------
                //draw external bar band                    
                // -----------------------------------
                   
                if (barOrientation == Orientation.Horizontal)
                {
                    #region horizontal
                    // Elapsed top
                    e.Graphics.DrawLine(new Pen(barPenColorPaintElapsedTop, 1f), barRect.X, barRect.Y - 1 + barRect.Height / 2, barRect.X + elapsedRect.Width, barRect.Y - 1 + barRect.Height / 2);
                    // Elapsed bottom
                    e.Graphics.DrawLine(new Pen(barPenColorPaintElapsedBottom, 1f), barRect.X, barRect.Y + 1 + barRect.Height / 2, barRect.X + elapsedRect.Width, barRect.Y + 1 + barRect.Height / 2);


                    // Remain top
                    e.Graphics.DrawLine(new Pen(barPenColorPaintRemainTop, 1f), barRect.X + elapsedRect.Width, barRect.Y - 1 + barRect.Height / 2, barRect.X + barRect.Width, barRect.Y - 1 + barRect.Height / 2);
                    // Remain bottom
                    e.Graphics.DrawLine(new Pen(barPenColorPaintRemainBottom, 1f), barRect.X + elapsedRect.Width, barRect.Y + 1 + barRect.Height / 2, barRect.X + barRect.Width, barRect.Y + 1 + barRect.Height / 2);


                    // Gauche vertical (foncé)
                    e.Graphics.DrawLine(new Pen(barPenColorPaintRemainTop, 1f), barRect.X, barRect.Y -1 + barRect.Height/2, barRect.X, barRect.Y + barRect.Height/2 + 1);

                    // Right vertical (clair)                        
                    e.Graphics.DrawLine(new Pen(barPenColorPaintRemainBottom, 1f), barRect.X + barRect.Width, barRect.Y - 1 + barRect.Height/2, barRect.X + barRect.Width, barRect.Y + 1 + barRect.Height/2);
                    #endregion
                }
                else
                {
                    #region vertical
                    // Elapsed top
                    e.Graphics.DrawLine(new Pen(barPenColorPaintElapsedTop, 1f), barRect.X -1 + barRect.Width/2, barRect.Y + (barRect.Height - elapsedRect.Height), barRect.X - 1 + barRect.Width / 2, barRect.Y + barRect.Height);

                    // Elapsed bottom
                    e.Graphics.DrawLine(new Pen(barPenColorPaintElapsedBottom, 1f), barRect.X + 1 + barRect.Width / 2, barRect.Y + (barRect.Height - elapsedRect.Height), barRect.X + 1 + barRect.Width/2, barRect.Y + barRect.Height);


                    // Remain top
                    e.Graphics.DrawLine(new Pen(barPenColorPaintRemainTop, 1f), barRect.X - 1 + barRect.Width / 2, barRect.Y, barRect.X - 1 + barRect.Width / 2, barRect.Y + barRect.Height - elapsedRect.Height);


                    // Remain bottom
                    e.Graphics.DrawLine(new Pen(barPenColorPaintRemainBottom, 1f), barRect.X + 1 + barRect.Width / 2, barRect.Y, barRect.X + 1 + barRect.Width / 2, barRect.Y + barRect.Height - elapsedRect.Height);


                    // haut horizontal (foncé) 
                    e.Graphics.DrawLine(new Pen(barPenColorPaintRemainTop, 1f), barRect.X - 1 + barRect.Width/2, barRect.Y, barRect.X + 1 + barRect.Width/2, barRect.Y);

                    // bas horizontal (clair)
                    e.Graphics.DrawLine(new Pen(barPenColorPaintRemainBottom, 1f), barRect.X - 1 + barRect.Width/2, barRect.Y + barRect.Height, barRect.X + 1 + barRect.Width/2, barRect.Y + barRect.Height);
                    #endregion

                }
                    
                #endregion draw contours

                

                #region draw thumb
                //draw thumb
                Color newthumbOuterColorPaint = thumbOuterColorPaint, newthumbInnerColorPaint = thumbInnerColorPaint;
                if (Capture && drawSemitransparentThumb)
                {
                    newthumbOuterColorPaint = Color.FromArgb(175, thumbOuterColorPaint);
                    newthumbInnerColorPaint = Color.FromArgb(175, thumbInnerColorPaint);
                }

                LinearGradientBrush lgbThumb;
                if (barOrientation == Orientation.Horizontal)
                {
                    lgbThumb = new LinearGradientBrush(thumbRect, newthumbOuterColorPaint, newthumbInnerColorPaint, gradientOrientation);
                }
                else
                {
                    lgbThumb = new LinearGradientBrush(thumbHalfRect, newthumbOuterColorPaint, newthumbInnerColorPaint, gradientOrientation);                    
                }
                using (lgbThumb)
                {
                    lgbThumb.WrapMode = WrapMode.TileFlipXY;
                    
                    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    e.Graphics.FillPath(lgbThumb, thumbPath);

                    //draw thumb band
                    Color newThumbPenColor = thumbPenColorPaint;

                    if (mouseEffects && (Capture || mouseInThumbRegion))
                        newThumbPenColor = ControlPaint.Dark(newThumbPenColor);
                    using (Pen thumbPen = new Pen(newThumbPenColor))
                    {

                        if (thumbImage != null)
                        {
                            Bitmap bmp = new Bitmap(thumbImage);
                            bmp.MakeTransparent(Color.FromArgb(255, 0, 255));
                            Rectangle srceRect = new Rectangle(0, 0, bmp.Width, bmp.Height);

                            e.Graphics.DrawImage(bmp, thumbRect, srceRect, GraphicsUnit.Pixel);
                            bmp.Dispose();
                            
                        }
                        else
                        {
                            e.Graphics.DrawPath(thumbPen, thumbPath);
                        }
                    }

                }
                #endregion draw thumb


                #region draw focusing rectangle
                //draw focusing rectangle
                if (Focused & drawFocusRectangle)
                    using (Pen p = new Pen(Color.FromArgb(200, barPenColorPaintElapsedTop)))
                    {
                        p.DashStyle = DashStyle.Dot;
                        Rectangle r = ClientRectangle;
                        r.Width -= 2;
                        r.Height--;
                        r.X++;
                                               
                        using (GraphicsPath gpBorder = CreateRoundRectPath(r, borderRoundRectSize))
                        {
                            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                            e.Graphics.DrawPath(p, gpBorder);
                        }
                    }
                #endregion draw focusing rectangle


                #region draw ticks
                if (tickStyle != TickStyle.None)
                {
                    int x1, x2, y1, y2 = 0;
                    int nbticks = 1 +  _scaleDivisions * (_scaleSubDivisions + 1);                    
                    int interval = 0;
                    int start = 0;
                    int W = 0;
                    float rulerValue = 0;

                    if (barOrientation == Orientation.Horizontal)
                    {
                        start = thumbRect.Width / 2;
                        W = barRect.Width - thumbRect.Width;
                        rulerValue = (float)_minimum;
                    }
                    else
                    {
                        start = thumbRect.Height / 2;
                        W = barRect.Height - thumbRect.Height;
                        rulerValue = (float)_maximum;
                    }

                    float incr = W / (nbticks - 1);

                    // pen for ticks
                    Pen penTickL = new Pen(tickColor, 1f);
                    Pen penTickS = new Pen(tickColor, 1f);
                    int idx = 0;
                    int scaleL = 5;
                    int scaleS = 3;


                    // strings graduations
                    float tx = 0;
                    float ty = 0;
                    float fSize = (float)(6F);                                        

                    int startDiv = 0;

                    Color _scaleColor = Color.White;
                    SolidBrush br = new SolidBrush(_scaleColor);

                    // Caluculate max size of text 
                    String str = String.Format("{0,0:D}", _maximum);
                    Font font = new Font(this.Font.FontFamily, fSize);
                    SizeF maxsize = e.Graphics.MeasureString(str, font);


                    float lineLeftX, lineRightX = 0;
                    lineLeftX = ClientRectangle.X + maxsize.Width/2;                   
                    lineRightX = ClientRectangle.X + ClientRectangle.Width - maxsize.Width/2;


                    for (int i = 0; i <= _scaleDivisions; i++)
                    {
                        // Calculate current text size
                        double val = Math.Round(rulerValue);
                        str = String.Format("{0,0:D}", (int)val);                                               
                        SizeF size = e.Graphics.MeasureString( str, font );                       

                        // HORIZONTAL
                        if (barOrientation == Orientation.Horizontal)
                        {
                            #region horizontal

                            // Draw string graduations
                            if (_showDivisionsText)
                            {
                                if (tickStyle == TickStyle.TopLeft || tickStyle == TickStyle.Both)
                                {
                                    tx = (start + barRect.X + interval) - (float)(size.Width * 0.5);
                                    ty = ClientRectangle.Y;
                                    e.Graphics.DrawString(str, font, br, tx, ty);
                                }
                                if (tickStyle == TickStyle.BottomRight || tickStyle == TickStyle.Both)
                                {
                                    tx = (start + barRect.X + interval) - (float)(size.Width * 0.5);
                                    ty = ClientRectangle.Y + ClientRectangle.Height - (size.Height) + 3;                                    
                                    e.Graphics.DrawString(str, font, br, tx, ty );
                                }

                                startDiv = (int)size.Height;
                            }

                            

                            // draw ticks                           
                            if (tickStyle == TickStyle.TopLeft || tickStyle == TickStyle.Both)
                            {                                 
                                x1 = start + barRect.X + interval;
                                y1 = ClientRectangle.Y + startDiv;
                                x2 = start + barRect.X + interval;
                                y2 = ClientRectangle.Y + startDiv + scaleL;
                                e.Graphics.DrawLine(penTickL, x1, y1, x2, y2);
                            }
                            if (tickStyle == TickStyle.BottomRight || tickStyle == TickStyle.Both)
                            {

                                x1 = start + barRect.X + interval;
                                y1 = ClientRectangle.Y + ClientRectangle.Height - startDiv;
                                x2 = start + barRect.X + interval;
                                y2 = ClientRectangle.Y + ClientRectangle.Height - scaleL - startDiv;

                                e.Graphics.DrawLine(penTickL, x1, y1, x2, y2);
                            }

                                  
                            rulerValue += (float)((_maximum - _minimum) / (_scaleDivisions));

                            // Draw subdivisions
                            if (i < _scaleDivisions)
                            {
                                for (int j = 0; j <= _scaleSubDivisions; j++)
                                {
                                    idx++;
                                    interval = idx * W / (nbticks - 1);

                                    if (_showSmallScale)
                                    {
                                        // Horizontal                            
                                        if (tickStyle == TickStyle.TopLeft || tickStyle == TickStyle.Both)
                                        {
                                            x1 = start + barRect.X + interval;
                                            y1 = ClientRectangle.Y + startDiv;
                                            x2 = start + barRect.X + interval;
                                            y2 = ClientRectangle.Y + startDiv + scaleS;
                                            e.Graphics.DrawLine(penTickS, x1, y1, x2, y2);
                                        }
                                        if (tickStyle == TickStyle.BottomRight || tickStyle == TickStyle.Both)
                                        {
                                            x1 = start + barRect.X + interval;
                                            y1 = ClientRectangle.Y + ClientRectangle.Height - startDiv;
                                            x2 = start + barRect.X + interval;
                                            y2 = ClientRectangle.Y + ClientRectangle.Height - scaleS - startDiv;

                                            e.Graphics.DrawLine(penTickS, x1, y1, x2, y2);
                                        }
                                    }
                                }
                            }
                            #endregion
                        }
                        else
                        {
                            #region vertical

                            // Draw string graduations
                            if (_showDivisionsText)
                            {                                
                                if (tickStyle == TickStyle.TopLeft || tickStyle == TickStyle.Both)
                                {                                    
                                    tx = lineLeftX - size.Width / 2;
                                    ty = start + barRect.Y + interval - (float)(size.Height * 0.5);
                                    e.Graphics.DrawString(str, font, br, tx, ty);
                                }
                                if (tickStyle == TickStyle.BottomRight || tickStyle == TickStyle.Both)
                                {                                    
                                    tx = lineRightX - size.Width / 2;
                                    ty = start + barRect.Y + interval - (float)(size.Height * 0.5);
                                    e.Graphics.DrawString(str, font, br, tx, ty);
                                }

                                startDiv = (int)maxsize.Width + 3;
                            }
                            

                            // draw ticks                            
                            if (tickStyle == TickStyle.TopLeft || tickStyle == TickStyle.Both)
                            {
                                x1 = ClientRectangle.X + startDiv;
                                y1 = start + barRect.Y + interval;
                                x2 = ClientRectangle.X + scaleL + startDiv;
                                y2 = start + barRect.Y + interval;
                                e.Graphics.DrawLine(penTickL, x1, y1, x2, y2);
                            }
                            if (tickStyle == TickStyle.BottomRight || tickStyle == TickStyle.Both)
                            {
                                x1 = ClientRectangle.X + ClientRectangle.Width - startDiv;
                                y1 = start + barRect.Y + interval;
                                x2 = ClientRectangle.X + ClientRectangle.Width - scaleL - startDiv;
                                y2 = start + barRect.Y + interval;
                                e.Graphics.DrawLine(penTickL, x1, y1, x2, y2);
                            }

                            rulerValue -= (float)((_maximum - _minimum) / (_scaleDivisions));

                            // draw subdivisions
                            if (i < _scaleDivisions)
                            {
                                for (int j = 0; j <= _scaleSubDivisions; j++)
                                {
                                    idx++;
                                    interval = idx * W / (nbticks - 1);

                                    if (_showSmallScale)
                                    {
                                        if (tickStyle == TickStyle.TopLeft || tickStyle == TickStyle.Both)
                                        {
                                            x1 = ClientRectangle.X + startDiv;
                                            y1 = start + barRect.Y + interval;
                                            x2 = ClientRectangle.X + scaleS + startDiv;
                                            y2 = start + barRect.Y + interval;
                                            e.Graphics.DrawLine(penTickS, x1, y1, x2, y2);
                                        }
                                        if (tickStyle == TickStyle.BottomRight || tickStyle == TickStyle.Both)
                                        {
                                            x1 = ClientRectangle.X + ClientRectangle.Width - startDiv;
                                            y1 = start + barRect.Y + interval;
                                            x2 = ClientRectangle.X + ClientRectangle.Width - scaleS - startDiv;
                                            y2 = start + barRect.Y + interval;
                                            e.Graphics.DrawLine(penTickS, x1, y1, x2, y2);
                                        }
                                    }
                                }
                            }

                            #endregion
                        }

                       

                    }
                }
                #endregion


            }
            catch (Exception Err)
            {
                Console.WriteLine("DrawBackGround Error in " + Name + ":" + Err.Message);
            }
            finally
            {
            }
        }

        #endregion

        #region Overided events

        private bool mouseInRegion = false;
        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.EnabledChanged"></see> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"></see> that contains the event data.</param>
        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            Invalidate();
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.MouseEnter"></see> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"></see> that contains the event data.</param>
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            mouseInRegion = true;
            Invalidate();
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.MouseLeave"></see> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"></see> that contains the event data.</param>
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            mouseInRegion = false;
            mouseInThumbRegion = false;
            Invalidate();
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.MouseDown"></see> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs"></see> that contains the event data.</param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left)
            {
                Capture = true;
                if (Scroll != null) Scroll(this, new ScrollEventArgs(ScrollEventType.ThumbTrack, trackerValue));
                if (ValueChanged != null) ValueChanged(this, new EventArgs());
                OnMouseMove(e);
            }
        }

        private bool mouseInThumbRegion = false;

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.MouseMove"></see> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs"></see> that contains the event data.</param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            mouseInThumbRegion = IsPointInRect(e.Location, thumbRect);
            if (Capture & e.Button == MouseButtons.Left)
            {
                ScrollEventType set = ScrollEventType.ThumbPosition;
                Point pt = e.Location;
                int p = barOrientation == Orientation.Horizontal ? pt.X : pt.Y;
                int margin = thumbSize >> 1;
                p -= margin;
                float coef = (float)(_maximum - _minimum) /
                             (float)
                             ((barOrientation == Orientation.Horizontal ? ClientSize.Width : ClientSize.Height) - 2 * margin);

                              
                trackerValue = barOrientation == Orientation.Horizontal ? (int)(p * coef + _minimum) : (_maximum - (int)(p * coef + _minimum));


                if (trackerValue <= _minimum)
                {
                    trackerValue = _minimum;
                    set = ScrollEventType.First;
                }
                else if (trackerValue >= _maximum)
                {
                    trackerValue = _maximum;
                    set = ScrollEventType.Last;
                }

                if (Scroll != null) Scroll(this, new ScrollEventArgs(set, trackerValue));
                if (ValueChanged != null) ValueChanged(this, new EventArgs());
            }
            Invalidate();
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.MouseUp"></see> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs"></see> that contains the event data.</param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            Capture = false;
            mouseInThumbRegion = IsPointInRect(e.Location, thumbRect);
            if (Scroll != null) Scroll(this, new ScrollEventArgs(ScrollEventType.EndScroll, trackerValue));
            if (ValueChanged != null) ValueChanged(this, new EventArgs());
            Invalidate();
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.MouseWheel"></see> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs"></see> that contains the event data.</param>
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            int v = e.Delta / 120 * (_maximum - _minimum) / mouseWheelBarPartitions;
            SetProperValue(Value + v);

            // FAB: 11/04/17 - avoid to send MouseWheel event to the parent container
            ((HandledMouseEventArgs)e).Handled = true;
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.GotFocus"></see> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"></see> that contains the event data.</param>
        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            Invalidate();
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.LostFocus"></see> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"></see> that contains the event data.</param>
        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            Invalidate();
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.KeyUp"></see> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs"></see> that contains the event data.</param>
        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            switch (e.KeyCode)
            {
                case Keys.Down:
                case Keys.Left:
                    SetProperValue(Value - (int)smallChange);
                    if (Scroll != null) Scroll(this, new ScrollEventArgs(ScrollEventType.SmallDecrement, Value));
                    break;
                case Keys.Up:
                case Keys.Right:
                    SetProperValue(Value + (int)smallChange);
                    if (Scroll != null) Scroll(this, new ScrollEventArgs(ScrollEventType.SmallIncrement, Value));
                    break;
                case Keys.Home:
                    Value = _minimum;
                    break;
                case Keys.End:
                    Value = _maximum;
                    break;
                case Keys.PageDown:
                    SetProperValue(Value - (int)largeChange);
                    if (Scroll != null) Scroll(this, new ScrollEventArgs(ScrollEventType.LargeDecrement, Value));
                    break;
                case Keys.PageUp:
                    SetProperValue(Value + (int)largeChange);
                    if (Scroll != null) Scroll(this, new ScrollEventArgs(ScrollEventType.LargeIncrement, Value));
                    break;
            }
            if (Scroll != null && Value == _minimum) Scroll(this, new ScrollEventArgs(ScrollEventType.First, Value));
            if (Scroll != null && Value == _maximum) Scroll(this, new ScrollEventArgs(ScrollEventType.Last, Value));
            Point pt = PointToClient(Cursor.Position);
            OnMouseMove(new MouseEventArgs(MouseButtons.None, 0, pt.X, pt.Y, 0));
        }

        /// <summary>
        /// Processes a dialog key.
        /// </summary>
        /// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys"></see> values that represents the key to process.</param>
        /// <returns>
        /// true if the key was processed by the control; otherwise, false.
        /// </returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Tab | ModifierKeys == Keys.Shift)
                return base.ProcessDialogKey(keyData);
            else
            {
                OnKeyDown(new KeyEventArgs(keyData));
                return true;
            }
        }

        #endregion

        #region Help routines

        /// <summary>
        /// Creates the round rect path.
        /// </summary>
        /// <param name="rect">The rectangle on which graphics path will be spanned.</param>
        /// <param name="size">The size of rounded rectangle edges.</param>
        /// <returns></returns>
        public static GraphicsPath CreateRoundRectPath(Rectangle rect, Size size)
        {
            GraphicsPath gp = new GraphicsPath();
            gp.AddLine(rect.Left + size.Width / 2, rect.Top, rect.Right - size.Width / 2, rect.Top);
            gp.AddArc(rect.Right - size.Width, rect.Top, size.Width, size.Height, 270, 90);

            gp.AddLine(rect.Right, rect.Top + size.Height / 2, rect.Right, rect.Bottom - size.Width / 2);
            gp.AddArc(rect.Right - size.Width, rect.Bottom - size.Height, size.Width, size.Height, 0, 90);

            gp.AddLine(rect.Right - size.Width / 2, rect.Bottom, rect.Left + size.Width / 2, rect.Bottom);
            gp.AddArc(rect.Left, rect.Bottom - size.Height, size.Width, size.Height, 90, 90);

            gp.AddLine(rect.Left, rect.Bottom - size.Height / 2, rect.Left, rect.Top + size.Height / 2);
            gp.AddArc(rect.Left, rect.Top, size.Width, size.Height, 180, 90);
            return gp;
        }

        /// <summary>
        /// Desaturates colors from given array.
        /// </summary>
        /// <param name="colorsToDesaturate">The colors to be desaturated.</param>
        /// <returns></returns>
        public static Color[] DesaturateColors(params Color[] colorsToDesaturate)
        {
            Color[] colorsToReturn = new Color[colorsToDesaturate.Length];
            for (int i = 0; i < colorsToDesaturate.Length; i++)
            {
                //use NTSC weighted avarage
                int gray =
                    (int)(colorsToDesaturate[i].R * 0.3 + colorsToDesaturate[i].G * 0.6 + colorsToDesaturate[i].B * 0.1);
                colorsToReturn[i] = Color.FromArgb(-0x010101 * (255 - gray) - 1);
            }
            return colorsToReturn;
        }

        /// <summary>
        /// Lightens colors from given array.
        /// </summary>
        /// <param name="colorsToLighten">The colors to lighten.</param>
        /// <returns></returns>
        public static Color[] LightenColors(params Color[] colorsToLighten)
        {
            Color[] colorsToReturn = new Color[colorsToLighten.Length];
            for (int i = 0; i < colorsToLighten.Length; i++)
            {
                colorsToReturn[i] = ControlPaint.Light(colorsToLighten[i]);
            }
            return colorsToReturn;
        }

        /// <summary>
        /// Sets the trackbar value so that it wont exceed allowed range.
        /// </summary>
        /// <param name="val">The value.</param>
        private void SetProperValue(int val)
        {
            if (val < _minimum) Value = _minimum;
            else if (val > _maximum) Value = _maximum;
            else Value = val;
        }

        /// <summary>
        /// Determines whether rectangle contains given point.
        /// </summary>
        /// <param name="pt">The point to test.</param>
        /// <param name="rect">The base rectangle.</param>
        /// <returns>
        /// 	<c>true</c> if rectangle contains given point; otherwise, <c>false</c>.
        /// </returns>
        private static bool IsPointInRect(Point pt, Rectangle rect)
        {
            if (pt.X > rect.Left & pt.X < rect.Right & pt.Y > rect.Top & pt.Y < rect.Bottom)
                return true;
            else return false;
        }

        #endregion
    }
}