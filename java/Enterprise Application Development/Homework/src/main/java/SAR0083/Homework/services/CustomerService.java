package SAR0083.Homework.services;

import java.util.List;

import javax.annotation.PostConstruct;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.stereotype.Service;

import SAR0083.Homework.models.Customer;
import SAR0083.Homework.repositories.CustomerRepository;
import SAR0083.Homework.repositories.CustomerRepositoryJdbc;
import SAR0083.Homework.repositories.CustomerRepositoryJpa;

@Service
public class CustomerService {
	// Properties
	private CustomerRepository customerRepository;
	@Autowired
	private CustomerRepositoryJpa customerRepositoryJpa;
	@Autowired
	private CustomerRepositoryJdbc customerRepositoryJdbc;
	@Value("${repository}")
	private String repository;
	
	
	// Repository
	@PostConstruct
	public void setRepository() {
		switch(repository) {
			case "jdbc":
				this.customerRepository = customerRepositoryJdbc;
				break;
			default:
				this.customerRepository = customerRepositoryJpa;
				break;
		}
	}
	
	
	// CRUD: read
	public List<Customer> getCustomers() {
		return customerRepository.getCustomers();
	}
	
	public Customer getCustomer(long id) {
		return customerRepository.getCustomer(id);
	}
	
	
	// CRUD: create + update
	public void setCustomer(Customer customer) {
		customerRepository.setCustomer(customer);
	}
	
	
	// CRUD: delete
	public void delete(long id) {
		customerRepository.delete(id);
	}
}