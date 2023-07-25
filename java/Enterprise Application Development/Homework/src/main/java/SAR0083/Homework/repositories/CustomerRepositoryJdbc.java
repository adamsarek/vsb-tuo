package SAR0083.Homework.repositories;

import java.util.List;

import javax.annotation.PostConstruct;
import javax.sql.DataSource;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.jdbc.core.JdbcTemplate;
import org.springframework.stereotype.Repository;

import SAR0083.Homework.models.Customer;

@Repository
public class CustomerRepositoryJdbc implements CustomerRepository {
	// Properties
	@Autowired
	private DataSource dataSource;
	private JdbcTemplate jdbcTemplate;
	
	
	// Constructor
	public CustomerRepositoryJdbc() {
		
	}
	
	
	// Methods: postConstruct
	@PostConstruct
	public void init() {
		jdbcTemplate = new JdbcTemplate(dataSource);
	}
	
	
	// CRUD: read
	@Override
	public List<Customer> getCustomers() {
		return jdbcTemplate.query(
			"SELECT * "
			+ "FROM Customer",
			new CustomerMapper()
		);
	}
	
	@Override
	public Customer getCustomer(long id) {
		return jdbcTemplate.queryForObject(
			"SELECT * "
			+ "FROM Customer "
			+ "WHERE id = ?",
			new CustomerMapper(),
			id
		);
	}
	
	
	// CRUD: create + update
	@Override
	public void setCustomer(Customer customer) {
		if(customer.getId() == 0) {
			jdbcTemplate.update(
				"INSERT INTO Customer ("
					+ "first_name, "
					+ "last_name, "
					+ "birth_date, "
					+ "email_local_part, "
					+ "email_domain, "
					+ "phone_country_code, "
					+ "phone_subscriber_number"
				+ ") VALUES (?, ?, ?, ?, ?, ?, ?)",
				customer.getFirstName(),
				customer.getLastName(),
				customer.getBirthDate(),
				customer.getEmailAddress().getLocalPart(),
				customer.getEmailAddress().getDomain(),
				customer.getPhoneNumber().getCountryCode(),
				customer.getPhoneNumber().getSubscriberNumber()
			);
		}
		else {
			jdbcTemplate.update(
				"UPDATE Customer "
				+ "SET "
					+ "first_name = ?, "
					+ "last_name = ?, "
					+ "birth_date = ?, "
					+ "email_local_part = ?, "
					+ "email_domain = ?, "
					+ "phone_country_code = ?, "
					+ "phone_subscriber_number = ? "
				+ "WHERE id = ?",
				customer.getFirstName(),
				customer.getLastName(),
				customer.getBirthDate(),
				customer.getEmailAddress().getLocalPart(),
				customer.getEmailAddress().getDomain(),
				customer.getPhoneNumber().getCountryCode(),
				customer.getPhoneNumber().getSubscriberNumber(),
				customer.getId()
			);
		}
	}
	
	
	// CRUD: delete
	public void delete(long id) {
		jdbcTemplate.update(
			"DELETE FROM Customer "
			+ "WHERE id = ?",
			id
		);
	}
}