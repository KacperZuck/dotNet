using AutoMapper;
using Health_App.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public abstract class ClassService<TEntity, TDto> : IService<TEntity, TDto>
        where TEntity : class, IEntity
        where TDto : class, IDto
    {
        protected readonly Health_App.Common.Repository<TEntity> _repository; 
        protected readonly IMapper _mapper;

        protected ClassService(Health_App.Common.Repository<TEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public virtual async Task<TDto?> Get(Guid id)
        {
            var entity = await _repository.Get(id);
            return _mapper.Map<TDto>(entity);
        }

        public virtual async Task<ICollection<TDto>> GetAll()
        {
            var entities = await _repository.GetAll();
            return _mapper.Map<ICollection<TDto>>(entities);
        }

        public virtual async Task Add(TDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            dto.id = Guid.NewGuid();
            var entity = _mapper.Map<TEntity>(dto);
            await _repository.Add(entity);
        }

        public virtual async Task Update(TDto dto)
        {
            var existingEntity = await _repository.Get(dto.id);
            if (existingEntity == null) throw new Exception("Item not found");

            _mapper.Map(dto, existingEntity);
            await _repository.Update(existingEntity);
        }

        public virtual async Task Delete(Guid id)
        {
            var dto = await _repository.Get(id);
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            await _repository.Delete(id);
        }
    }
}