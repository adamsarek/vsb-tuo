package SAR0083.Homework.repositories;

import java.util.List;

import org.springframework.stereotype.Repository;

import SAR0083.Homework.models.Lease;

@Repository
public interface LeaseRepository {
	// CRUD: read
	List<Lease> getLeases();
	
	Lease getLease(long id);
	
	
	// CRUD: create + update
	void setLease(Lease lease);
	
	
	// CRUD: delete
	void delete(long id);
}