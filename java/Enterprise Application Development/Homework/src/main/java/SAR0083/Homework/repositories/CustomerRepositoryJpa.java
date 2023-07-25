package SAR0083.Homework.repositories;

import java.util.List;

import javax.persistence.EntityManager;
import javax.persistence.PersistenceContext;
import javax.transaction.Transactional;

import org.springframework.stereotype.Repository;

import SAR0083.Homework.models.Customer;

@Repository
public class CustomerRepositoryJpa implements CustomerRepository {
	// Properties
	@PersistenceContext
	private EntityManager em;
	
	
	// CRUD: read
	@Override
	public List<Customer> getCustomers() {
		return em.createQuery(
			"SELECT customer "
			+ "FROM Customer customer",
			Customer.class
		).getResultList();
	}
	
	@Override
	public Customer getCustomer(long id) {
		return em.find(Customer.class, id);
	}
	
	
	// CRUD: create + update
	@Override
	@Transactional
	public void setCustomer(Customer customer) {
		if(customer.getId() == 0) {
			em.persist(customer);
		}
		else {
			em.merge(customer);
		}
	}
	
	
	// CRUD: delete
	@Override
	@Transactional
	public void delete(long id) {
		if(id != 0) {
			Customer customer = getCustomer(id);
			em.remove(customer);
		}
	}
}