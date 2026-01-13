using System;

public class Aluno
{
    public string Nome { get; set; } = string.Empty;
    public double[] Notas { get; set; } = new double[5];
    public double Frequencia { get; set; }
    
    //robustez para previnir erros de calculo causados pela entrada.
    public double MediaNotas => Notas.Length > 0 ? Notas.Average() : 0;
}
