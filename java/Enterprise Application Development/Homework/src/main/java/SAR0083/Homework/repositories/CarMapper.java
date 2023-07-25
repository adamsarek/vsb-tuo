package SAR0083.Homework.repositories;

import java.sql.ResultSet;
import java.sql.SQLException;

import org.springframework.jdbc.core.RowMapper;

import SAR0083.Homework.models.Car;
import SAR0083.Homework.models.Trunk;

public class CarMapper implements RowMapper<Car> {
	// Methods: override
	@Override
	public Car mapRow(ResultSet rs, int rowNum) throws SQLException {
		Car car = new Car();
		
		try {
			car.setId(rs.getLong("id"));
			car.setBrand(rs.getString("brand"));
			car.setModel(rs.getString("model"));
			car.setModelYear(rs.getShort("model_year"));
			car.setWeight(rs.getFloat("weight"));
			car.setTrunk(Trunk.fromInteger(rs.getInt("trunk")));
		} catch (Exception e) {
			e.printStackTrace();
		}
		
		return car;
	}
}