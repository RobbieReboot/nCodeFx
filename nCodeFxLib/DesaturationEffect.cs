﻿using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace nCodeFxLib
{


    /// <summary>
    /// Desaturates the attached visual element
    /// </summary>
    /// <remarks>
    /// Desaturates a visual element according to the desaturation paramter, where 0.0 (default) is
    /// saturated (i.e. untouched) and 1.0 represents full desaturation.
    /// </remarks>
    public class DesaturationEffect : ShaderEffect
    {
        #region Constructors

        /// <summary>
        /// Initializes the <see cref="DesaturationEffect"/> class.
        /// </summary>
        static DesaturationEffect()
        {
            _pixelShader.UriSource = Global.MakePackUri("DesaturationEffect.ps");
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DesaturationEffect"/> class.
        /// </summary>
        public DesaturationEffect()
        {
            this.PixelShader = _pixelShader;

            // Update each DependencyProperty that's registered with a shader register.  This
            // is needed to ensure the shader gets sent the proper default value.
            UpdateShaderValue(InputProperty);
            UpdateShaderValue(DesaturationProperty);
            UpdateShaderValue(IgnoreOpacityProperty);
            UpdateShaderValue(IgnoredOpacityValueProperty);
        }

        #endregion

        #region Dependency Properties

        /// <summary>
        /// The property for the Implicit input to the bitmap effect.
        /// </summary>
        /// <remarks>this is automatically passed by the framework and doesn't need to be set explicitly.</remarks>
        public Brush Input
        {
            get { return (Brush)GetValue(InputProperty); }
            set { SetValue(InputProperty, value); }
        }

        /// <summary>
        /// The dependency property for the Implicit input to the bitmap effect.
        /// </summary>
        /// <remarks>this is automatically passed by the framwork and doesn't need to be set explicitly.</remarks>        
        public static readonly DependencyProperty InputProperty =
            ShaderEffect.RegisterPixelShaderSamplerProperty("Input", typeof(DesaturationEffect), 0);

        /// <summary>
        /// Gets or sets the desaturation.
        /// </summary>
        /// <value>The desaturation amount that the element will desaturate by. 0.0 is the default which represents no desaturation, whereas 1.0 represents 
        /// full desaturation.</value>
        public float Desaturation
        {
            get { return (float)GetValue(DesaturationProperty); }
            set { SetValue(DesaturationProperty, value); }
        }

        /// <summary>
        /// The DependencyProperty for setting the Desaturation
        /// </summary>
        public static readonly DependencyProperty DesaturationProperty =
            DependencyProperty.Register("Desaturation", typeof(float), typeof(DesaturationEffect),
                    new UIPropertyMetadata(0.0f, PixelShaderConstantCallback(0)));

        /// <summary>
        /// Gets or sets the value representing an opacity value to ignore when desaturating.
        /// </summary>
        /// <remarks>This value allows the setting of any element within the effects visual tree to be excluded from the desaturation operation.
        /// Coupled with the IgnoredOpacityValue which represents that elements REAL opacity value, you can desaturate elements but
        /// still maintain the flexibilty to set the ignored items opacity values.</remarks>
        /// <value>By Default the Ignore Opacity is set to 0.0 which turn off this feature. Any other value will cause the desaturate effect to exclude pixels 
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
            DependencyProperty.Register("IgnoreOpacity", typeof(float), typeof(DesaturationEffect),
                    new UIPropertyMetadata(0.0f, PixelShaderConstantCallback(1)));


        /// <summary>
        /// Gets or sets the ignored opacity value.
        /// </summary>
        /// <remarks>This represents the actual opacity a pixel will be set to if it was ignored by the effect. Since the only way we can exclude an item from the
        /// Colorizing effect is to set a known 'sentinel' opacity value which the effect can then ignore, we need someway of still being able to set this elements 
        /// opacity values. The IgnoredOpacityValue is the opacity the effect will use when it comes across a pixel with the Ignored Opacity. Note that the
        /// IgnoredOpacityValue can only ever reach the maximum set by the elements Real Opacity. If the elements Opactity is set to "0.7" then a value of "1.0" for
        /// the IgnoredOpacityValue will translate to the 0.7 of the actual opacity. The largest sentinel value settable for the Opacity to be ignored is 0.995</remarks>
        public float IgnoredOpacityValue
        {
            get { return (float)GetValue(IgnoredOpacityValueProperty); }
            set { SetValue(IgnoredOpacityValueProperty, value); }
        }


        /// <summary>
        /// Dependcency Property for the IgnoredOpacityValue.
        /// </summary>
        public static readonly DependencyProperty IgnoredOpacityValueProperty =
            DependencyProperty.Register("IgnoredOpacityValue", typeof(float), typeof(DesaturationEffect),
                    new UIPropertyMetadata(1.0f, PixelShaderConstantCallback(2)));



        #endregion

        #region Member Data

        private static PixelShader _pixelShader = new PixelShader();

        #endregion

    }
}
