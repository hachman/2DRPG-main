using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ModuleController : MonoBehaviour
{
    [SerializeField] TMP_Text Title;
    [SerializeField] TMP_Text ModuleTitle;
    [SerializeField] TMP_Text Content;

    string[] title = new string[] { 
        "MATH ANGAT MODULE I",
        "MATH ANGAT MODULE II"
    };
    string[] moduleTitle = new string[] {
        "Quadratic Equations",
        "Rational Algebraic Equations"
    };
    string[] content = new string[] {
        "Definition:\r\nA quadratic equation is a second-order polynomial equation in a single variable \\(x\\). It has the general form: [ ax^2 + bx + c = 0] \r\nwhere (a), (b), and (c) are constants with (a not equal 0). The term (ax^2) is the quadratic term, (bx) is the linear term, and (c) is the constant term.\r\n\r\nMethods for Solving Quadratic Equations\r\nFactoring \r\nFactoring involves expressing the quadratic equation as a product of two binomial expressions. If a quadratic equation can be factored, it takes the form: [ (px + q)(rx + s) = 0 ] Solving for (x) involves setting each binomial equal to zero and solving for the variable.\r\n\r\nExample 1: [ x^2 - 5x + 6 = 0] \r\nExample 2: [ 2x^2 - 8x + 6 = 0] \r\nUsing the Quadratic Formula \r\nThe quadratic formula is derived from the process of completing the square and provides a universal method for solving any quadratic equation. The quadratic formula is:\r\n\r\nExample 3: [ x^2 - 4x - 5 = 0]\r\n\r\nExample 4: [ 3x^2 + 2x - 1 = 0] \r\n\r\nCompleting the Square Completing the square involves manipulating the equation so that one side of the equation becomes a perfect square trinomial.\r\nExample 5: [ x^2 + 6x + 5 = 0]\r\nExample 6: [ 2x^2 + 8x + 6 = 0]\r\n",
        "Definition:\r\nA rational algebraic equation is an equation that involves rational expressions, which are fractions where the numerator and/or the denominator are polynomials. The general form of a rational algebraic equation is: p ( x ) q ( x ) where and are polynomials and q ( x ) ≠ 0 . The parent graph for rational functions is y = 1 x , and the shape is called a hyperbola.\r\n\r\nMethods for Solving Rational Algebraic Equations:\r\n\r\nFactoring \r\nFactoring involves simplifying the rational expression by factoring both the numerator and the denominator and then solving the resulting polynomial equation.\r\nThe Rational Root Theorem \r\nThe Rational Root Theorem provides a method to find all possible rational roots of a polynomial equation \\(P(x) = 0\\), which helps in solving the rational algebraic equation.\r\nSynthetic Division \r\nSynthetic division is a simplified form of polynomial division, which is used to divide polynomials and to find roots of polynomial equations.\r\nExample:\r\nSolve (2x^3 - 3x^2 - 8x + 3 = 0) using synthetic division.\r\n"
    };
  
    public void setModuleContent(string sceneName)
    {
        int i = sceneName == "Forest" ? 0 : 1;

        Title.text = title[i];
        ModuleTitle.text = title[i];
        Content.text = content[i];
    }
}
