﻿using Scaffold.Core.Abstract;
using Scaffold.Core.Attributes;
using Scaffold.Core.CalcValues;
using Scaffold.Core.Images.Models;
using Scaffold.Core.Models;
using SkiaSharp;

namespace Scaffold.XUnitTests.Core;

[CalcMetadata("Add values", "Core library tester")]
public class AdditionCalculation : CalculationBase
{
    public CalcDouble LeftAssignment { get; set; }
    public CalcDouble RightAssignment { get; set; }
    public CalcDouble Result { get; set; }

    private double Add()
        => LeftAssignment.Value + RightAssignment.Value;
        
    protected override void DefineInputs()
    {
        LeftAssignment = new CalcDouble("Left assignment", 2);
        RightAssignment = new CalcDouble("Right assignment", 3);
    }

    protected override void DefineOutputs()
    {
        Result = new CalcDouble("Result", Add());
    }

    public override void Update()
    {
        Result.Value = Add();
    }

    protected override IEnumerable<Formula> GenerateFormulae()
    {
        // TODO: Fix FromBitmap.
        // var keyImage = new SKBitmap(1000,200);
        // using (var canvas = new SKCanvas(keyImage))
        // {
        //     var paintText = new SKPaint { TextSize = 25, TextAlign = SKTextAlign.Left };
        //     var paint = new SKPaint { Color = SKColors.Black, IsAntialias = true };
        //     paint.IsStroke = true; paint.StrokeWidth = 5;
        //     
        //     canvas.DrawCircle(550, 175, 15, paint);
        //     canvas.DrawCircle(600, 175, 15, paint);
        //     canvas.DrawCircle(650, 175, 15, paint);
        //     canvas.DrawText("Shear links", 750, 185, paintText);
        // }

        var list = new List<Formula>
        {
            Formula.New("Narrative to appear above the expression")
                .WithConclusion("Some text here")
                .WithReference("Some ref here")
                .AddExpression("x &=& a + b")
                .AddImage(new ImageFromEmbeddedResource("ImageAsEmbeddedResource.png")),
            
            new("2. Some ref here", "2. Narrative to appear above the expression", "2. Some text here", @"\alpha_1=\left[ \frac{35}{f_{cm}} \right]^{0.7}"),
            
            Formula.New("Final narrative")
                .WithReference("3.a")
                .AddImage(new ImageFromRelativePath("ImageAsRelativePath.png"))
        };

        return list;
    }
}