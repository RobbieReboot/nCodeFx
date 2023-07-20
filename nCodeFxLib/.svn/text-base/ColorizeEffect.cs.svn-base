using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace nCodeFxLib
{

    /// <summary>
    /// Colorize Effect.
    /// </summary>
    /// <remarks>The Colorize effect changes all the colours to match the Color property. Each pixels
    /// liminance value is used to determine the overall value of the resulting pixel, so a luminance value of
    /// white will return the full intensity of the Colour Parameter. 
    /// <example><code>
    /// &lt;StackPanel Orientation="Vertical"&gt;
    ///     &lt;StackPanel.Effect&gt;
    ///     &lt;nCodeFxLib:ColorizeEffect Color="BlanchedAlmond"/&gt;
    ///     &lt;/StackPanel.Effect&gt;
    ///     &lt;!--All UIElements in this stack panel will now be recoloured to BlanchedAlmond--&gt;
    /// &lt;/StackPanel&gt;
    /// </code></example>
    /// 
    /// <example><code>
    /// &lt;StackPanel Orientation="Vertical"&gt;
    ///     &lt;StackPanel.Effect&gt;
    ///     &lt;nCodeFxLib:ColorizeEffect Color="BlanhedAlmond" IgnoreOpacity="0.995"/&gt;
    ///     &lt;/StackPanel.Effect&gt;
    ///     &lt;!--All UIElements in this stack panel will be recoloured to BlanchedAlmond--&gt;
    ///     &lt;!--Except for this button                                                 --&gt;
    ///     &lt;Button Opacity="0.995"&gt;A button excluded from the effect&lt;/Button&gt;
    /// &lt;/StackPanel&gt;
    /// </code></example>
    /// </remarks>
    public class ColorizeEffect : ShaderEffect
    {
        #region Constructors

        static ColorizeEffect()
        {
            _pixelShader.UriSource = Global.MakePackUri("ColorizeEffect.ps");
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ColorizeEffect"/> class.
        /// </summary>
        /// <remarks>All properties are set to defaults of :
        /// Color = "White",
        /// IgnoreOpacity = "0.0" ( disabled ),
        /// IgnoredOpacityValue = "1.0" ( full opacity after the effect is applied)
        /// </remarks>
        public ColorizeEffect()
        {
            this.PixelShader = _pixelShader;

            // Update each DependencyProperty that's registered with a shader register.  This
            // is needed to ensure the shader gets sent the proper default value.
            UpdateShaderValue(InputProperty);
            UpdateShaderValue(ColorProperty);
            UpdateShaderValue(IgnoreOpacityProperty);
            UpdateShaderValue(IgnoredOpacityValueProperty);
        }

        #endregion

        #region Dependency Properties

        /// <summary>
        /// Gets or sets the input.
        /// </summary>
        /// <value>The implicit imput property for the. This is automatically passed by the framework and shouldn't be set in your code.</value>
        public Brush Input{
            get { return (Brush)GetValue(InputProperty); }
            set { SetValue(InputProperty, value); }
        }

        /// <summary>
        /// The dependency property for the Implicit input to the bitmap effect.
        /// </summary>
        /// <remarks>this is automatically passed by the framwork and doesn't need to be set explicitly.</remarks>
        public static readonly DependencyProperty InputProperty =
            ShaderEffect.RegisterPixelShaderSamplerProperty("Input", typeof(ColorizeEffect), 0);


        /// <summary>
        /// Gets or sets the color which will be used to colorize the visual element the effect is applied to.
        /// </summary>
        /// <remarks>Any colour value can be passed, however some will work better than others. Typically the darker colours will produce variable results.
        /// Most light colours will produce pleasing results. When using the IgnoreOpacity feature to allow, for example, the text on buttons to remain
        /// uncoloured, using darker colours will show up the text's anti aliased outline making the text look unsightly. This artifacting doesn't show up on
        /// lighter colours.
        /// Colours can be set from the <see cref="Color"/> class, or can be set directly with the Hex colour format eg. "#FF34E023". If you're using the 
        /// IgnoreOpacity feature, using the Hex colour value will allow passing the opacity value to ignore. </remarks>
        public Color Color {
            get { return (Color)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }

        /// <summary>
        /// The Dependency property representing the Color Property.
        /// </summary>
        /// <remarks>The dependency property means you can set bindings in your XAML to the colour property.</remarks>
        public static readonly DependencyProperty ColorProperty =
            DependencyProperty.Register("Color", typeof(Color), typeof(ColorizeEffect),
                    new UIPropertyMetadata(Colors.White, PixelShaderConstantCallback(0)));

       
        /// <summary>
        /// Gets or sets the value representing an opacity value to ignore when colorizing.
        /// </summary>
        /// <remarks>This value allows the setting of any element within the pixel shaders influence to be excluded from the shading operation.
        /// Coupled with the IgnoredOpacityValue which represents that elements REAL opacity value, you can colourize the interface ignoring elements but
        /// still maintain the flexibilty to set the ignored items opacity values.</remarks>
        /// <value>By Default the Ignore Opacity is set to 0.0 which turn off this feature. Any other value will cause the colorize effect to exclude pixels 
        /// with this Opacity value.</value>
        public float IgnoreOpacity
        {
            get { return (float)GetValue(IgnoreOpacityProperty); }
            set { SetValue(IgnoreOpacityProperty, value); }
        }

        /// <summary>
        /// Dependency Property for the IgnoreOpacity property.
        /// </summary>
        /// <remarks>IgnoreOpacity dependency property means you can bind to this property within your XAML code.</remarks>
        public static readonly DependencyProperty IgnoreOpacityProperty =
            DependencyProperty.Register("IgnoreOpacity", typeof(float), typeof(ColorizeEffect),
                    new UIPropertyMetadata(0.0f, PixelShaderConstantCallback(1)));


        /// <summary>
        /// Gets or sets the ignored opacity value.
        /// </summary>
        /// <remarks>This represents the actual opacity a pixel will be set to if it was ignored by the pixel shader. Since the only way we can exclude an item from the
        /// Colorizing effect is to set a known 'sentinel' alpha value which the colorize effect can then ignore, we need someway of still being able to set this elements 
        /// opacity values. The IgnoredOpacityValue </remarks>
        public float IgnoredOpacityValue
        {
            get { return (float)GetValue(IgnoredOpacityValueProperty); }
            set { SetValue(IgnoredOpacityValueProperty, value); }
        }


        /// <summary>
        /// Dependcency Property for the IgnoredOpacityValue.
        /// </summary>
        public static readonly DependencyProperty IgnoredOpacityValueProperty =
            DependencyProperty.Register("IgnoredOpacityValue", typeof(float), typeof(ColorizeEffect),
                    new UIPropertyMetadata(1.0f, PixelShaderConstantCallback(2)));


        #endregion

        #region Member Data

        private static PixelShader _pixelShader = new PixelShader();

        #endregion

    }
}
