package SAR0083.Homework.repositories;

import java.util.List;

import javax.annotation.PostConstruct;
import javax.sql.DataSource;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.jdbc.core.JdbcTemplate;
import org.springframework.stereotype.Repository;

import SAR0083.Homework.models.Vehicle;
import SAR0083.Homework.models.Customer;
import SAR0083.Homework.models.Lease;

@Repository
public class LeaseRepositoryJdbc implements LeaseRepository {
	// Properties
	@Autowired
	private DataSource dataSource;
	private JdbcTemplate jdbcTemplate;
	
	
	// Constructor
	public LeaseRepositoryJdbc() {
		
	}
	
	
	// Methods: postConstruct
	@PostConstruct
	public void init() {
		jdbcTemplate = new JdbcTemplate(dataSource);
	}
	
	
	// CRUD: read
	@Override
	public List<Lease> getLeases() {
		List<Lease> leases = jdbcTemplate.query(
			"SELECT * "
			+ "FROM Lease",
			new LeaseMapper()
		);
		
		for(int i = 0; i < leases.size(); i++) {
			Lease lease = leases.get(i);
			
			Vehicle vehicle = jdbcTemplate.queryForObject(
				"SELECT * "
				+ "FROM Vehicle "
				+ "WHERE id = ?",
				new VehicleMapper(),
				lease.getVehicle().getId()
			);
			lease.setVehicle(vehicle);
			
			Customer customer = jdbcTemplate.queryForObject(
				"SELECT * "
				+ "FROM Customer "
				+ "WHERE id = ?",
				new CustomerMapper(),
				lease.getCustomer().getId()
			);
			lease.setCustomer(customer);
			
			leases.set(i, lease);
		}
		
		return leases;
	}
	
	@Override
	public Lease getLease(long id) {
		Lease lease = jdbcTemplate.queryForObject(
			"SELECT * "
			+ "FROM Lease "
			+ "WHERE id = ?",
			new LeaseMapper(),
			id
		);
		
		Vehicle vehicle = jdbcTemplate.queryForObject(
			"SELECT * "
			+ "FROM Vehicle "
			+ "WHERE id = ?",
			new VehicleMapper(),
			lease.getVehicle().getId()
		);
		lease.setVehicle(vehicle);
		
		Customer customer = jdbcTemplate.queryForObject(
			"SELECT * "
			+ "FROM Customer "
			+ "WHERE id = ?",
			new CustomerMapper(),
			lease.getCustomer().getId()
		);
		lease.setCustomer(customer);
		
		return lease;
	}
	
	
	// CRUD: create + update
	@Override
	public void setLease(Lease lease) {
		if(lease.getId() == 0) {
			jdbcTemplate.update(
				"INSERT INTO Lease ("
					+ "start_date, "
					+ "end_date, "
					+ "insurance, "
					+ "vehicle_id, "
					+ "customer_id"
				+ ") VALUES (?, ?, ?, ?, ?)",
				lease.getStartDate(),
				lease.getEndDate(),
				lease.getInsurance(),
				lease.getVehicle().getId(),
				lease.getCustomer().getId()
			);
		}
		else {
			jdbcTemplate.update(
				"UPDATE Lease "
				+ "SET "
					+ "start_date = ?, "
					+ "end_date = ?, "
					+ "insurance = ?, "
					+ "vehicle_id = ?, "
					+ "customer_id = ? "
				+ "WHERE id = ?",
				lease.getStartDate(),
				lease.getEndDate(),
				lease.getInsurance(),
				lease.getVehicle().getId(),
				lease.getCustomer().getId(),
				lease.getId()
			);
		}
	}
	
	
	// CRUD: delete
	public void delete(long id) {
		jdbcTemplate.update(
			"DELETE FROM Lease "
			+ "WHERE id = ?",
			id
		);
	}
}