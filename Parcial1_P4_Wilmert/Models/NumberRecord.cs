namespace Parcial1_P4_Wilmert.Models;

public record NumberRecord(int Id, DateTime Fecha, int Numero, long Resultado)
{
       public NumberRecord() : this(0, default, 0, 0) { }

}