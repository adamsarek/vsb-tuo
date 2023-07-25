package SAR0083.Homework.repositories;

import java.sql.ResultSet;
import java.sql.SQLException;

import org.springframework.jdbc.core.RowMapper;

import SAR0083.Homework.models.Vehicle;
import SAR0083.Homework.models.Customer;
import SAR0083.Homework.models.Lease;
import SAR0083.Homework.services.VehicleService;
import SAR0083.Homework.services.CustomerService;

public class LeaseMapper implements RowMapper<Lease> {
	// Properties
	VehicleService vehicleService = new VehicleService();
	CustomerService customerService = new CustomerService();
	
	
	// Methods: override
	@Override
	public Lease mapRow(ResultSet rs, int rowNum) throws SQLException {
		Lease lease = new Lease();
		
		try {
			lease.setId(rs.getLong("id"));
			lease.setStartDate(rs.getDate("start_date").toLocalDate());
			lease.setEndDate(rs.getDate("end_date").toLocalDate());
			lease.setInsurance(rs.getBoolean("insurance"));
			
			Vehicle vehicle = new Vehicle();
			vehicle.setId(rs.getLong("vehicle_id"));
			lease.setVehicle(vehicle);
			
			Customer customer = new Customer();
			customer.setId(rs.getLong("customer_id"));
			lease.setCustomer(customer);
		} catch (Exception e) {
			e.printStackTrace();
		}
		
		return lease;
	}
}