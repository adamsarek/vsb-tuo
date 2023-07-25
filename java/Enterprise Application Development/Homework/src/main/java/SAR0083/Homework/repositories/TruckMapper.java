package SAR0083.Homework.repositories;

import java.sql.ResultSet;
import java.sql.SQLException;

import org.springframework.jdbc.core.RowMapper;

import SAR0083.Homework.models.Truck;

public class TruckMapper implements RowMapper<Truck> {
	// Methods: override
	@Override
	public Truck mapRow(ResultSet rs, int rowNum) throws SQLException {
		Truck truck = new Truck();
		
		try {
			truck.setId(rs.getLong("id"));
			truck.setBrand(rs.getString("brand"));
			truck.setModel(rs.getString("model"));
			truck.setModelYear(rs.getShort("model_year"));
			truck.setWeight(rs.getFloat("weight"));
			truck.setTrailerCount(rs.getShort("trailer_count"));
		} catch (Exception e) {
			e.printStackTrace();
		}
		
		return truck;
	}
}