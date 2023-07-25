package SAR0083.Homework.repositories;

import java.util.List;

import javax.annotation.PostConstruct;
import javax.sql.DataSource;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.jdbc.core.JdbcTemplate;
import org.springframework.stereotype.Repository;

import SAR0083.Homework.models.Car;
import SAR0083.Homework.models.Truck;
import SAR0083.Homework.models.Vehicle;

@Repository
public class VehicleRepositoryJdbc implements VehicleRepository {
	// Properties
	@Autowired
	private DataSource dataSource;
	private JdbcTemplate jdbcTemplate;
	
	
	// Constructor
	public VehicleRepositoryJdbc() {
		
	}
	
	
	// Methods: postConstruct
	@PostConstruct
	public void init() {
		jdbcTemplate = new JdbcTemplate(dataSource);
	}
	
	
	// CRUD: read
	@Override
	public List<Vehicle> getVehicles() {
		return jdbcTemplate.query(
			"SELECT * "
			+ "FROM Vehicle",
			new VehicleMapper()
		);
	}
	
	@Override
	public List<Car> getCars() {
		return jdbcTemplate.query(
			"SELECT * "
			+ "FROM Vehicle "
			+ "WHERE vehicle_type = ?",
			new CarMapper(),
			1
		);
	}
	
	@Override
	public List<Truck> getTrucks() {
		return jdbcTemplate.query(
			"SELECT * "
			+ "FROM Vehicle "
			+ "WHERE vehicle_type = ?",
			new TruckMapper(),
			2
		);
	}
	
	@Override
	public Vehicle getVehicle(long id) {
		return jdbcTemplate.queryForObject(
			"SELECT * "
			+ "FROM Vehicle "
			+ "WHERE id = ?",
			new VehicleMapper(),
			id
		);
	}
	
	@Override
	public Car getCar(long id) {
		return jdbcTemplate.queryForObject(
			"SELECT * "
			+ "FROM Vehicle "
			+ "WHERE id = ?",
			new CarMapper(),
			id
		);
	}
	
	@Override
	public Truck getTruck(long id) {
		return jdbcTemplate.queryForObject(
			"SELECT * "
			+ "FROM Vehicle "
			+ "WHERE id = ?",
			new TruckMapper(),
			id
		);
	}
	
	
	// CRUD: create + update
	@Override
	public void setCar(Car car) {
		if(car.getId() == 0) {
			jdbcTemplate.update(
				"INSERT INTO Vehicle ("
					+ "brand, "
					+ "model, "
					+ "model_year, "
					+ "weight, "
					+ "vehicle_type, "
					+ "trunk"
				+ ") VALUES (?, ?, ?, ?, ?, ?)",
				car.getBrand(),
				car.getModel(),
				car.getModelYear(),
				car.getWeight(),
				1,
				car.getTrunk().getValue()
			);
		}
		else {
			jdbcTemplate.update(
				"UPDATE Vehicle "
				+ "SET "
					+ "brand = ?, "
					+ "model = ?, "
					+ "model_year = ?, "
					+ "weight = ?, "
					+ "trunk = ? "
				+ "WHERE id = ?",
				car.getBrand(),
				car.getModel(),
				car.getModelYear(),
				car.getWeight(),
				car.getTrunk().getValue(),
				car.getId()
			);
		}
	}
	
	@Override
	public void setTruck(Truck truck) {
		if(truck.getId() == 0) {
			jdbcTemplate.update(
				"INSERT INTO Vehicle ("
					+ "brand, "
					+ "model, "
					+ "model_year, "
					+ "weight, "
					+ "vehicle_type, "
					+ "trailer_count"
				+ ") VALUES (?, ?, ?, ?, ?, ?)",
				truck.getBrand(),
				truck.getModel(),
				truck.getModelYear(),
				truck.getWeight(),
				2,
				truck.getTrailerCount()
			);
		}
		else {
			jdbcTemplate.update(
				"UPDATE Vehicle "
				+ "SET "
					+ "brand = ?, "
					+ "model = ?, "
					+ "model_year = ?, "
					+ "weight = ?, "
					+ "trailer_count = ? "
				+ "WHERE id = ?",
				truck.getBrand(),
				truck.getModel(),
				truck.getModelYear(),
				truck.getWeight(),
				truck.getTrailerCount(),
				truck.getId()
			);
		}
	}
	
	
	// CRUD: delete
	public void delete(long id) {
		jdbcTemplate.update(
			"DELETE FROM Vehicle "
			+ "WHERE id = ?",
			id
		);
	}
}