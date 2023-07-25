package SAR0083.Homework.services;

import java.util.List;

import javax.annotation.PostConstruct;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.stereotype.Service;

import SAR0083.Homework.models.Car;
import SAR0083.Homework.models.Truck;
import SAR0083.Homework.models.Vehicle;
import SAR0083.Homework.repositories.VehicleRepository;
import SAR0083.Homework.repositories.VehicleRepositoryJdbc;
import SAR0083.Homework.repositories.VehicleRepositoryJpa;

@Service
public class VehicleService {
	// Properties
	private VehicleRepository vehicleRepository;
	@Autowired
	private VehicleRepositoryJpa vehicleRepositoryJpa;
	@Autowired
	private VehicleRepositoryJdbc vehicleRepositoryJdbc;
	@Value("${repository}")
	private String repository;
	
	
	// Repository
	@PostConstruct
	public void setRepository() {
		switch(repository) {
			case "jdbc":
				this.vehicleRepository = vehicleRepositoryJdbc;
				break;
			default:
				this.vehicleRepository = vehicleRepositoryJpa;
				break;
		}
	}
	
	
	// CRUD: read
	public List<Vehicle> getVehicles() {
		return vehicleRepository.getVehicles();
	}
	
	public List<Car> getCars() {
		return vehicleRepository.getCars();
	}
	
	public List<Truck> getTrucks() {
		return vehicleRepository.getTrucks();
	}
	
	public Vehicle getVehicle(long id) {
		return vehicleRepository.getVehicle(id);
	}
	
	public Car getCar(long id) {
		return vehicleRepository.getCar(id);
	}
	
	public Truck getTruck(long id) {
		return vehicleRepository.getTruck(id);
	}
	
	
	// CRUD: create + update
	public void setCar(Car car) {
		vehicleRepository.setCar(car);
	}
	
	public void setTruck(Truck truck) {
		vehicleRepository.setTruck(truck);
	}
	
	
	// CRUD: delete
	public void delete(long id) {
		vehicleRepository.delete(id);
	}
}