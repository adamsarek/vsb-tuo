package SAR0083.Homework.repositories;

import java.util.List;

import javax.persistence.EntityManager;
import javax.persistence.PersistenceContext;
import javax.transaction.Transactional;

import org.springframework.stereotype.Repository;

import SAR0083.Homework.models.Lease;

@Repository
public class LeaseRepositoryJpa implements LeaseRepository {
	// Properties
	@PersistenceContext
	private EntityManager em;
	
	
	// CRUD: read
	@Override
	public List<Lease> getLeases() {
		return em.createQuery(
			"SELECT lease "
			+ "FROM Lease lease",
			Lease.class
		).getResultList();
	}
	
	@Override
	public Lease getLease(long id) {
		return em.find(Lease.class, id);
	}
	
	
	// CRUD: create + update
	@Override
	@Transactional
	public void setLease(Lease lease) {
		if(lease.getId() == 0) {
			em.persist(lease);
		}
		else {
			em.merge(lease);
		}
	}
	
	
	// CRUD: delete
	@Override
	@Transactional
	public void delete(long id) {
		if(id != 0) {
			Lease lease = getLease(id);
			em.remove(lease);
		}
	}
}