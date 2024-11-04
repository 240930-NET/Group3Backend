using ABCDoubleE.Models;
using ABCDoubleE.Repositories;
using System.Threading.Tasks;

namespace ABCDoubleE.Services
{
    public class PreferenceService : IPreferenceService
    {
        private readonly IPreferenceRepository _preferenceRepository;

        public PreferenceService(IPreferenceRepository preferenceRepository)
        {
            _preferenceRepository = preferenceRepository;
        }

        public async Task<Preference> GetPreferenceByUserIdAsync(int userId)
        {
            return await _preferenceRepository.GetPreferenceByUserIdAsync(userId);
        }

        public async Task<Preference> CreatePreferenceAsync(int userId)
        {
            var preference = new Preference { userId = userId };
            return await _preferenceRepository.CreatePreferenceAsync(preference);
        }

        public async Task UpdatePreferenceAsync(Preference preference)
        {
            await _preferenceRepository.UpdatePreferenceAsync(preference);
        }

        public async Task DeletePreferenceByUserIdAsync(int userId)
        {
            await _preferenceRepository.DeletePreferenceByUserIdAsync(userId);
        }

    }
}
