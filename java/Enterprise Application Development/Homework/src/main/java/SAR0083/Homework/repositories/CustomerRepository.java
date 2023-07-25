package SAR0083.Homework.repositories;

import java.util.List;

import org.springframework.stereotype.Repository;

import SAR0083.Homework.models.Customer;

@Repository
public interface CustomerRepository {
	// CRUD: read
	List<Customer> getCustomers();
	
	Customer getCustomer(long id);
	
	
	// CRUD: create + update
	void setCustomer(Customer customer);
	
	
	// CRUD: delete
	void delete(long id);
}