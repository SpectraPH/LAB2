namespace ConsoleApp2.elements
{
    public class Trap : Element
    {
        public Trap()
        {
            sym = 'x';
        }
        
        
        public override char getElement()
        {
            return sym;
        }
    }
}