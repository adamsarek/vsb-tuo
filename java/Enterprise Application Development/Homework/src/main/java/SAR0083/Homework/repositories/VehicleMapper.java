package SAR0083.Homework.repositories;

import java.sql.ResultSet;
import java.sql.SQLException;

import org.springframework.jdbc.core.RowMapper;

import SAR0083.Homework.models.Vehicle;

public class VehicleMapper implements RowMapper<Vehicle> {
	// Methods: override
	@Override
	public Vehicle mapRow(ResultSet rs, int rowNum) throws SQLException {
		Vehicle vehicle = new Vehicle();
		
		try {
			vehicle.setId(rs.getLong("id"));
			vehicle.setBrand(rs.getString("brand"));
			vehicle.setModel(rs.getString("model"));
			vehicle.setModelYear(rs.getShort("model_year"));
			vehicle.setWeight(rs.getFloat("weight"));
		} catch (Exception e) {
			e.printStackTrace();
		}
		
		return vehicle;
	}
}