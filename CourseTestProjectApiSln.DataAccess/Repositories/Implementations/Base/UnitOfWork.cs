using CourseTestProjectApiSln.DataAccess.Data;
using CourseTestProjectApiSln.DataAccess.Repositories.Abstractions.Base;
using Microsoft.EntityFrameworkCore;




namespace CourseTestProjectApiSln.DataAccess.Repositories.Implementations.Base
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApiCourseSystem _context;
        private Dictionary<Type, IRepositoryBase> _repositories;

        public UnitOfWork(ApiCourseSystem context)
        {
            _context = context;
            _repositories = new Dictionary<Type, IRepositoryBase>();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        public TRepository GetRepository<TRepository>() where TRepository : class, IRepositoryBase
        {
            Type repositoryImplementationType = GetConcreteRepositoryInfo<TRepository>();
            if (_repositories.TryGetValue(repositoryImplementationType, out IRepositoryBase existingREpository))
            {
                return (TRepository)existingREpository;
            }

            IRepositoryBase? repositoryInstance = (IRepositoryBase)Activator.CreateInstance(repositoryImplementationType, _context);

            if (repositoryInstance is TRepository repository)
            {
                _repositories.Add(repositoryImplementationType, repository);
                return repository;
            }

            throw new InvalidOperationException($"Could not create repository of type {repositoryImplementationType.FullName}");
        }

        private Type GetConcreteRepositoryInfo<TRepository>()
        {
            string interfaceName = typeof(TRepository).Name;
            string className = interfaceName.StartsWith("I") ? interfaceName.Substring(1) : interfaceName;

            string interfaceNamespace = typeof(TRepository).Namespace;
            string implementationFullName = $"{interfaceNamespace}.{className}";
            Type repositoryType = Type.GetType(implementationFullName);
            if (repositoryType == null)
            {
                repositoryType = AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetTypes()).FirstOrDefault(t => t.Name == className && typeof(TRepository).IsAssignableFrom(t));
            }
            if (repositoryType == null)
            {
                throw new InvalidOperationException($"No concrete repository class found for {typeof(TRepository).Name}");
            }

            return repositoryType;
        }
    }
}