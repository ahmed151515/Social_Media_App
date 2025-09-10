namespace Core.Interfaces;

public interface ISoftDeleteable
{
	public bool IsDeleted { get; set; }
	public DateTime? DeleteDate { get; set; }

	public void Delete();
}