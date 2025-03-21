using AutoMapper;
using RotaViagem.Domain;
using RotaViagem.Repository.IRepository;
using RotaViagem.Service.Dtos;
using RotaViagem.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotaViagem.Service
{
    public class LocalService : ILocalService
    {
        private readonly ILocalRepository _localRepository;
        private readonly IMapper _mapper;
        public LocalService(ILocalRepository localRepository,
                                  IMapper mapper)
        {
            _localRepository = localRepository;
            _mapper = mapper;
        }

        public async Task<LocalDto> Add(LocalAddDto model)
        {
            try
            {
                var local = _mapper.Map<Local>(model);
                _localRepository.Add<Local>(local);

                if (await _localRepository.SaveChangesAsync())
                {
                    return _mapper.Map<LocalDto>(local);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<LocalDto> Update(LocalUpdateDto model)
        {
            try
            {
                var local = await _localRepository.GetByIdAsync(model.Id);
                if (local == null) return null;

                model.Id = local.Id;

                _mapper.Map(model, local);

                _localRepository.Update<Local>(local);

                if (await _localRepository.SaveChangesAsync())
                {
                    return _mapper.Map<LocalDto>(local);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var local = await _localRepository.GetByIdAsync(id);
                if (local == null) throw new Exception("Lote para delete não encontrado.");

                _localRepository.Delete<Local>(local);
                return await _localRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IList<LocalDto>> GetAllAsync()
        {
            try
            {
                var locals = await _localRepository.GetAllAsync();
                if (locals == null) return null;

                return _mapper.Map<IList<LocalDto>>(locals);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<LocalDto> GetByIdAsync(int id)
        {
            try
            {
                var local = await _localRepository.GetByIdAsync(id);
                if (local == null) return null;

                return _mapper.Map<LocalDto>(local);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<LocalDto> GetByNomeAsync(string nome)
        {
            try
            {
                var local = await _localRepository.GetByNomeAsync(nome);
                if (local == null) return null;

                return _mapper.Map<LocalDto>(local);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
