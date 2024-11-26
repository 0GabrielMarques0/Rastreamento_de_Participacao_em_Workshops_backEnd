public class Colaborador
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public List<Ata> Atas { get; set; } = new List<Ata>();

}

