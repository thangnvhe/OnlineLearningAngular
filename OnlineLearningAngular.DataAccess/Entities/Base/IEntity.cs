namespace OnlineLearningAngular.DataAccess.Entities.Base
{
    public interface IEntity<TKey>
    {
        TKey Id { get; set; } 
    }
}
