namespace GenericInterfaces
{
    //Decided to separate this in a specific layer since we can expect it to be used by layers other than
    //Repository in the future, for example for mapping domain models to DTOs
    public interface IMapper<TSource, TDestination>
    {
        TDestination Map(TSource source);
    }
}