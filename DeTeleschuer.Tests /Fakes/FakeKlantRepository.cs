using Interface.Dtos;
using Interface.Repositories;

namespace DeTeleschuer.Tests.Fakes;

public class FakeKlantRepository : IKlantRepository
{
    private readonly int? _bestaandId;

    public bool OpslaanAangeroepen { get; private set; }
    public int TeruggegvenId { get; } = 42;

    public FakeKlantRepository(int? bestaandId = null)
    {
        _bestaandId = bestaandId;
    }

    public int Opslaan(KlantDto klant)
    {
        OpslaanAangeroepen = true;
        return TeruggegvenId;
    }

    public int? HaalIdOpViaEmail(string email) => _bestaandId;
}
