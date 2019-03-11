namespace ConsoleApp2.elements
{
    public class Coin : Element
    {
        public Coin()
        {
            sym = '0';
        }
        
        public override char getElement()
        {
            return sym;
        }
    }
}