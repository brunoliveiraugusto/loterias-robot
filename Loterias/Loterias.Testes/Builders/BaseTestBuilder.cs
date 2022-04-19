namespace Loterias.Test.Builders
{
    public abstract class BaseTestBuilder<M>
    {
        protected M Model;

        public M Build()
        {
            return Model;
        }
    }
}
