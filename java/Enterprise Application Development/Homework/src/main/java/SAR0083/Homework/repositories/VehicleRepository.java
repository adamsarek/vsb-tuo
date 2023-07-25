package SAR0083.Homework.repositories;

import java.util.List;

import org.springframework.stereotype.Repository;

import SAR0083.Homework.models.Car;
import SAR0083.Homework.models.Truck;
import SAR0083.Homework.models.Vehicle;

@Repository
public interface VehicleRepository {
	// CRUD: read
	List<Vehicle> getVehicles();
	
	List<Car> getCars();
	
	List<Truck> getTrucks();
	
	Vehicle getVehicle(long id);
	
	Car getCar(long id);
	
	Truck getTruck(long id);
	
	
	// CRUD: create + update
	void setCar(Car car);
	
	void setTruck(Truck truck);
	
	
	// CRUD: delete
	void delete(long id);
}