using System.ComponentModel.DataAnnotations;

namespace OnlineLearningAngular.DataAccess.Entities.Base
{

    public abstract class BaseEntity<TKey> : IEntity<TKey>
    {
        [Key]
        public TKey Id { get; set; } = default!;
    }
}
