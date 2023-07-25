package SAR0083.Homework.repositories;

import java.util.List;

import javax.persistence.EntityManager;
import javax.persistence.PersistenceContext;
import javax.transaction.Transactional;

import org.springframework.stereotype.Repository;

import SAR0083.Homework.models.Car;
import SAR0083.Homework.models.Truck;
import SAR0083.Homework.models.Vehicle;

@Repository
public class VehicleRepositoryJpa implements VehicleRepository {
	// Properties
	@PersistenceContext
	private EntityManager em;
	
	
	// CRUD: read
	@Override
	public List<Vehicle> getVehicles() {
		return em.createQuery(
			"SELECT vehicle "
			+ "FROM Vehicle vehicle",
			Vehicle.class
		).getResultList();
	}
	
	@Override
	public List<Car> getCars() {
		return em.createQuery(
			"SELECT car "
			+ "FROM Car car",
			Car.class
		).getResultList();
	}
	
	@Override
	public List<Truck> getTrucks() {
		return em.createQuery(
			"SELECT truck "
			+ "FROM Truck truck",
			Truck.class
		).getResultList();
	}
	
	@Override
	public Vehicle getVehicle(long id) {
		return em.find(Vehicle.class, id);
	}
	
	@Override
	public Car getCar(long id) {
		return em.find(Car.class, id);
	}
	
	@Override
	public Truck getTruck(long id) {
		return em.find(Truck.class, id);
	}
	
	
	// CRUD: create + update
	@Override
	@Transactional
	public void setCar(Car car) {
		if(car.getId() == 0) {
			em.persist(car);
		}
		else {
			em.merge(car);
		}
	}
	
	@Override
	@Transactional
	public void setTruck(Truck truck) {
		if(truck.getId() == 0) {
			em.persist(truck);
		}
		else {
			em.merge(truck);
		}
	}
	
	
	// CRUD: delete
	@Override
	@Transactional
	public void delete(long id) {
		if(id != 0) {
			Vehicle vehicle = getVehicle(id);
			em.remove(vehicle);
		}
	}
}